import React from 'react';
import { connect } from 'react-redux';
import * as yup from 'yup';
import ContentHeader from '../../components/ContentHeader';
import { Form, Button } from 'react-bootstrap';
import * as actions from '../../store/actions/index';
import { Redirect } from 'react-router-dom';
import { useForm } from 'react-hook-form'

function TransactionsNew(props) {
    const formSchema = yup.object({
        name: yup.string().required().max(1000),
        description: yup.string().max(2000),
        date: yup.date()
      });
    const { register, handleSubmit, errors } = useForm({
        validationSchema: formSchema,
        defaultValues: { title: '', description: '', date: '', categoryId: 0, tagIds: [], sourceAccountId: 0, destinationAccountId: 0, amount: 0 }
    });
    const onSubmit = (data) => {
        props.onSubmitTransaction(data);
    };

    if(props.added)
        return (<Redirect to="/transactions" />);
    
    return (
        <div>
            <ContentHeader>
                <h1 className="h2">New Transaction</h1>
            </ContentHeader>
            <Form noValidate onSubmit={handleSubmit(onSubmit)}>
                <Form.Group>
                    <Form.Label>Name</Form.Label>
                    <Form.Control
                        type="text"
                        name="name"
                        isInvalid={!!errors.name} />
                    <Form.Control.Feedback type="invalid">
                        {errors.name}
                    </Form.Control.Feedback>
                </Form.Group>
                <Form.Group>
                    <Form.Label>Description</Form.Label>
                    <Form.Control
                        type="text"
                        name="description"
                        isInvalid={!!errors.description} />
                    <Form.Control.Feedback type="invalid">
                        {errors.description}
                    </Form.Control.Feedback>
                </Form.Group>
                <Form.Group>
                    <Form.Label>Date</Form.Label>
                    <Form.Control
                        type="date"
                        name="date"
                        isInvalid={!!errors.date} />
                    <Form.Control.Feedback type="invalid">
                        {errors.date}
                    </Form.Control.Feedback>
                </Form.Group>
                <Button type="submit" variant="primary" disabled={props.loading}>Submit</Button>
            </Form>
        </div>
    );
}

const mapStateToProps = state => {
    return {
      loading: state.transaction.loading,
      added: state.transaction.submitted
    };
  };

const mapDispatchToProps = dispatch => {
  return {
    onSubmitTransaction: (transactionData) =>
      dispatch(actions.addTransaction(transactionData))
  };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
  )(TransactionsNew);