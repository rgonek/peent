import React, { useEffect, useState } from "react";
import { connect } from "react-redux";
import * as yup from "yup";
import ContentHeader from "../../components/ContentHeader";
import { Form, Button, Col } from "react-bootstrap";
import * as actions from "../../store/actions/index";
import { Redirect } from "react-router-dom";
import { useForm, Controller } from "react-hook-form";
import Select from "../../components/UI/Select/Select";
import DateTimePicker from "../../components/UI/DateTimePicker/DateTimePicker";
import { AccountType, TransactionType } from "../../shared/constants";
import * as _ from "../../shared/extensions";

function TransactionsNew(props) {
    const formSchema = yup.object({
        title: yup.string().required().max(1000),
        description: yup.string().max(2000),
        date: yup.date(),
        categoryId: yup.number().required().min(1),
        tagIds: yup.array(),
        sourceAccountId: yup.number().required().min(1),
        destinationAccountId: yup.number().required().min(1),
        amount: yup.number().positive().required(),
    });
    const onCategoriesInputChange = (inputValue) => {
        if (!props.allCategoriesLoaded)
            props.onFetchCategoriesOptions(inputValue);
    };
    const onTagsInputChange = (inputValue) => {
        if (!props.allTagsLoaded) props.onFetchTagsOptions(inputValue);
    };
    const onAccountsInputChange = (inputValue) => {
        if (!props.allAccountsLoaded) props.onFetchAccountsOptions(inputValue);
    };
    const { register, control, setValue, handleSubmit, errors } = useForm({
        validationSchema: formSchema,
        defaultValues: {
            title: "",
            description: "",
            date: "",
            categoryId: 0,
            tagIds: [],
            sourceAccountId: 0,
            destinationAccountId: 0,
            amount: 0,
        },
    });

    const [sourceAccountType, setSourceAccountType] = useState(null);
    const [destinationAccountType, setDestinationAccountType] = useState(null);

    useEffect(() => {
        onCategoriesInputChange();
        onTagsInputChange();
        onAccountsInputChange();
    }, []);

    const handleSourceAccountChange = (value, action) => {
        setSourceAccountType(
            props.accounts.find((x) => x.id === value?.value)?.type
        );
    };
    const handleDestinationAccountChange = (value, action) => {
        setDestinationAccountType(
            props.accounts.find((x) => x.id === value?.value)?.type
        );
    };

    const onSubmit = (data) => {
        console.log("submit");
        console.log(data);
        //props.onSubmitTransaction(data);
    };

    const categoriesOptions = props.categories.toOptions(
        (x) => x.name,
        (x) => x.id
    );
    const tagsOptions = props.tags.toOptions(
        (x) => x.name,
        (x) => x.id
    );
    const sourceAccountsOptions = filterSourceAccounts(
        props.accounts,
        destinationAccountType
    )
        .groupBy((x) => x.type)
        .toList()
        .toOptions(
            (x) => x + " accounts",
            (x) =>
                x.toOptions(
                    (x) => x.name,
                    (x) => x.id
                )
        );
    const destinationAccountsOptions = filterDestinationAccounts(
        props.accounts,
        sourceAccountType
    )
        .groupBy((x) => x.type)
        .toList()
        .toOptions(
            (x) => x + " accounts",
            (x) =>
                x.toOptions(
                    (x) => x.name,
                    (x) => x.id
                )
        );

    const transactionType = getTransactionType(
        sourceAccountType,
        destinationAccountType
    );

    if (props.added) return <Redirect to="/transactions" />;

    return (
        <div>
            <ContentHeader>
                <h1 className="h2">New Transaction</h1>
            </ContentHeader>
            <Form onSubmit={handleSubmit(onSubmit)}>
                <Form.Group>
                    <Form.Label>Date</Form.Label>
                    <DateTimePicker />
                </Form.Group>
                <Form.Row>
                    <Form.Group as={Col}>
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

                    <Form.Group as={Col}>
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
                </Form.Row>

                <Form.Row>
                    <Form.Group as={Col}>
                        <Form.Label>Source account</Form.Label>
                        <Select
                            name="sourceAccountId"
                            isInvalid={!!errors.sourceAccountId}
                            options={sourceAccountsOptions}
                            onChange={handleSourceAccountChange}
                            onInputChange={onAccountsInputChange}
                            isLoading={props.accountsLoading}
                            control={control}
                            placeholder="Select source account"
                        />
                        <Form.Control.Feedback type="invalid">
                            {errors.sourceAccountId &&
                                errors.sourceAccountId.message}
                        </Form.Control.Feedback>
                    </Form.Group>

                    <Form.Group as={Col}>
                        <Form.Label>{transactionType}</Form.Label>
                    </Form.Group>
                </Form.Row>

                <Form.Row>
                    <Form.Group as={Col}>
                        <Form.Label>Destination account</Form.Label>
                        <Select
                            name="destinationAccountId"
                            isInvalid={!!errors.destinationAccountId}
                            options={destinationAccountsOptions}
                            onChange={handleDestinationAccountChange}
                            onInputChange={onAccountsInputChange}
                            isLoading={props.accountsLoading}
                            control={control}
                            placeholder="Select destination account"
                        />
                        <Form.Control.Feedback type="invalid">
                            {errors.destinationAccountId &&
                                errors.destinationAccountId.message}
                        </Form.Control.Feedback>
                    </Form.Group>

                    <Form.Group as={Col}>
                        <Form.Label>Amount</Form.Label>
                        <Form.Control
                            type="number"
                            name="amount"
                            isInvalid={!!errors.amount}
                            ref={register}
                        />
                        <Form.Control.Feedback type="invalid">
                            {errors.amount && errors.amount.message}
                        </Form.Control.Feedback>
                    </Form.Group>
                </Form.Row>

                <Form.Row>
                    <Form.Group as={Col}>
                        <Form.Label>Description</Form.Label>
                        <Form.Control
                            as="textarea"
                            rows="5"
                            name="description"
                            isInvalid={!!errors.description}
                            ref={register}
                        />
                        <Form.Control.Feedback type="invalid">
                            {errors.description && errors.description.message}
                        </Form.Control.Feedback>
                    </Form.Group>

                    <Col>
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
                    </Col>
                </Form.Row>

                <Button
                    type="submit"
                    variant="primary"
                    disabled={props.loading}
                >
                    Submit
                </Button>
            </Form>
        </div>
    );
}

const mapStateToProps = (state) => {
    return {
        loading: state.transaction.loading,
        added: state.transaction.submitted,
        categoriesLoading: state.category.loading,
        categories: state.category.categories,
        // TODO: if won't work properly if there is more than 100 categories (pageSize limit)
        allCategoriesLoaded: state.category.pageCount === 1,
        tagsLoading: state.tag.loading,
        tags: state.tag.tags,
        allTagsLoaded: state.tag.pageCount === 1,
        accountsLoading: state.account.loading,
        accounts: state.account.accounts,
        allAccountsLoaded: state.account.pageCount === 1,
    };
};

const mapDispatchToProps = (dispatch) => {
    return {
        onSubmitTransaction: (transactionData) =>
            dispatch(actions.addTransaction(transactionData)),
        onFetchCategoriesOptions: (search) =>
            dispatch(actions.fetchCategoriesOptions(search)),
        onFetchTagsOptions: (search) =>
            dispatch(actions.fetchTagsOptions(search)),
        onFetchAccountsOptions: (search) =>
            dispatch(actions.fetchAccountsOptions(search)),
    };
};

const filterSourceAccounts = (accounts, destinationAccountType) => {
    return accounts.filter(
        (x) =>
            x.type === AccountType.asset ||
            (destinationAccountType !== AccountType.expense &&
                x.type === AccountType.revenue)
    );
};

const filterDestinationAccounts = (accounts, sourceAccountType) => {
    return accounts.filter(
        (x) =>
            x.type === AccountType.asset ||
            (sourceAccountType !== AccountType.revenue &&
                x.type === AccountType.expense)
    );
};

const getTransactionType = (sourceAccountType, destinationAccountType) => {
    if (sourceAccountType === AccountType.asset)
        return destinationAccountType == AccountType.asset
            ? TransactionType.transfer
            : TransactionType.withdrawal;

    if (sourceAccountType == AccountType.revenue)
        return TransactionType.deposit;

    if (sourceAccountType == AccountType.initialBalance)
        return TransactionType.openingBalance;

    if (sourceAccountType == AccountType.reconciliation)
        return TransactionType.reconciliation;

    return TransactionType.unknown;
};

export default connect(mapStateToProps, mapDispatchToProps)(TransactionsNew);
