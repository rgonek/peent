import React, { useEffect } from "react";
import { connect } from "react-redux";
import * as yup from "yup";
import ContentHeader from "../../components/ContentHeader";
import { Form, Button } from "react-bootstrap";
import * as actions from "../../store/actions/index";
import { Redirect } from "react-router-dom";
import { useForm, Controller } from "react-hook-form";
import Select from "../../components/UI/Select/Select";
import ReactSelect from "react-select";

function TransactionsNew(props) {
  const formSchema = yup.object({
    title: yup
      .string()
      .required()
      .max(1000),
    description: yup.string().max(2000),
    date: yup.date(),
    categoryId: yup
      .number()
      .required()
      .min(1),
    tagIds: yup.array()
    // sourceAccountId: yup.number().required().min(1),
    // destinationAccountId: yup.number().required().min(1),
    // amount: yup.number().required()
  });
  const onCategoriesInputChange = inputValue => {
    if (!props.allCategoriesLoaded) props.onFetchCategoriesOptions(inputValue);
  };
  const onTagsInputChange = inputValue => {
    if (!props.allTagsLoaded) props.onFetchTagsOptions(inputValue);
  };
  const { register, control, setValue, handleSubmit, errors } = useForm({
    validationSchema: formSchema,
    defaultValues: {
      title: "",
      description: "",
      date: "",
      categoryId: 0//,
      // tagIds: [] //, sourceAccountId: 0, destinationAccountId: 0, amount: 0
    }
  });
  useEffect(() => {
    onCategoriesInputChange();
    onTagsInputChange();
  }, []);
  const onSubmit = data => {
    console.log("submit");
    console.log(data);
    //props.onSubmitTransaction(data);
  };
  const handleChange = (value, action) => {
      setValue("categoryId", value?.value);
  };

  const categoriesOptions = props.categories.map(category => ({
    label: category.name,
    value: category.id
  }));
  const tagsOptions = props.tags.map(tag => ({
    label: tag.name,
    value: tag.id
  }));

  if (props.added) return <Redirect to="/transactions" />;

  return (
    <div>
      <ContentHeader>
        <h1 className="h2">New Transaction</h1>
      </ContentHeader>
      <Form onSubmit={handleSubmit(onSubmit)}>
        <Form.Group>
          <Form.Label>Title</Form.Label>
          <Form.Control
            type="text"
            name="title"
            isInvalid={!!errors.title}
            ref={register}
          />
          <Form.Control.Feedback type="invalid">
            {errors.title && errors.title.message}
          </Form.Control.Feedback>
        </Form.Group>
        <Form.Group>
          <Form.Label>Description</Form.Label>
          <Form.Control
            type="text"
            name="description"
            isInvalid={!!errors.description}
            ref={register}
          />
          <Form.Control.Feedback type="invalid">
            {errors.description && errors.description.message}
          </Form.Control.Feedback>
        </Form.Group>
        <Form.Group>
          <Form.Label>Date</Form.Label>
          <Form.Control
            type="date"
            name="date"
            isInvalid={!!errors.date}
            ref={register}
          />
          <Form.Control.Feedback type="invalid">
            {errors.date && errors.date.message}
          </Form.Control.Feedback>
        </Form.Group>
        <Form.Group>
          <Form.Label>Category</Form.Label>
          <Select
            name="categoryId"
            isInvalid={!!errors.categoryId}
            options={categoriesOptions}
            onInputChange={onCategoriesInputChange}
            isLoading={props.categoriesLoading}
            control={control}
            placeholder="Select category"
          />
          <Form.Control.Feedback type="invalid">
            {errors.categoryId && errors.categoryId.message}
          </Form.Control.Feedback>
        </Form.Group>
        <Form.Group>
          <Form.Label>Tags</Form.Label>
          <Select
            name="tagIds"
            isInvalid={!!errors.tagIds}
            options={tagsOptions}
            isMulti={true}
            onInputChange={onTagsInputChange}
            isLoading={props.tagsLoading}
            control={control}
            placeholder="Select tags"
          />
          <Form.Control.Feedback type="invalid">
            {errors.tagIds && errors.tagIds.message}
          </Form.Control.Feedback>
        </Form.Group>
        <Button type="submit" variant="primary" disabled={props.loading}>
          Submit
        </Button>
      </Form>
    </div>
  );
}

const mapStateToProps = state => {
  return {
    loading: state.transaction.loading,
    added: state.transaction.submitted,
    categoriesLoading: state.category.loading,
    categories: state.category.categories,
    // TODO: if won't work properly if there is more than 100 categories (pageSize limit)
    allCategoriesLoaded: state.category.pageCount === 1,
    tagsLoading: state.tag.loading,
    tags: state.tag.tags,
    allTagsLoaded: state.tag.pageCount === 1
  };
};

const mapDispatchToProps = dispatch => {
  return {
    onSubmitTransaction: transactionData =>
      dispatch(actions.addTransaction(transactionData)),
    onFetchCategoriesOptions: search =>
      dispatch(actions.fetchCategoriesOptions(search)),
    onFetchTagsOptions: search =>
      dispatch(actions.fetchTagsOptions(search))
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(TransactionsNew);
