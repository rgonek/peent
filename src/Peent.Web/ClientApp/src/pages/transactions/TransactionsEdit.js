import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { connect } from "react-redux";
import * as yup from "yup";
import ContentHeader from "../../components/ContentHeader";
import Spinner from "../../components/UI/Spinner/Spinner";
import { Form, Button, Col } from "react-bootstrap";
import * as actions from "../../store/actions/index";
import { Redirect } from "react-router-dom";
import { useForm } from "react-hook-form";
import Select from "../../components/UI/Select/Select";
import DateTimePicker from "../../components/UI/DateTimePicker/DateTimePicker";
import NumberInput from "../../components/UI/NumberInput/NumberInput";
import { AccountType, TransactionType } from "../../shared/constants";
import PropTypes from "prop-types";
import "../../shared/extensions";

function TransactionsEdit({
    transaction,
    loading,
    added,
    categoriesLoading,
    categories,
    allCategoriesLoaded,
    tagsLoading,
    tags,
    allTagsLoaded,
    accountsLoading,
    accounts,
    allAccountsLoaded,
    onSubmitTransaction,
    onFetchTransaction,
    onFetchCategoriesOptions,
    onFetchTagsOptions,
    onFetchAccountsOptions,
}) {
    const { id } = useParams();
    useEffect(() => {
        onFetchTransaction(id);
    }, [id, onFetchTransaction]);

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
        if (!allCategoriesLoaded) onFetchCategoriesOptions(inputValue);
    };
    const onTagsInputChange = (inputValue) => {
        if (!allTagsLoaded) onFetchTagsOptions(inputValue);
    };
    const onAccountsInputChange = (inputValue) => {
        if (!allAccountsLoaded) onFetchAccountsOptions(inputValue);
    };
    const { register, control, handleSubmit, errors, reset } = useForm({
        validationSchema: formSchema,
        defaultValues: {
            title: transaction?.title,
            description: transaction?.description,
            date: transaction?.date,
            categoryId: transaction?.category.id,
            tagIds: transaction?.tags.map((x) => x.id) ?? [],
            sourceAccountId: transaction?.sourceAccount.id,
            destinationAccountId: transaction?.destinationAccount.id,
            amount: transaction?.amount,
        },
    });
    useEffect(() => {
        if (transaction) {
            var val = {
                title: transaction?.title,
                description: transaction?.description,
                date: transaction?.date.toString(),
                categoryId: transaction?.category.id,
                tagIds: transaction?.tags.map((x) => x.id) ?? [],
                sourceAccountId: transaction?.sourceAccount.id,
                destinationAccountId: transaction?.destinationAccount.id,
                amount: transaction?.amount,
            };
            reset(val);
        }
    }, [transaction, reset]);

    const [sourceAccountType, setSourceAccountType] = useState(null);
    const [destinationAccountType, setDestinationAccountType] = useState(null);

    useEffect(() => {
        onCategoriesInputChange();
        onTagsInputChange();
        onAccountsInputChange();
    });

    const handleSourceAccountChange = (value) => {
        setSourceAccountType(accounts.find((x) => x.id === value?.value)?.type);
    };
    const handleDestinationAccountChange = (value) => {
        setDestinationAccountType(accounts.find((x) => x.id === value?.value)?.type);
    };

    const onSubmit = (data) => {
        onSubmitTransaction(id, data);
    };

    const categoriesOptions = categories.toOptions(
        (x) => x.name,
        (x) => x.id
    );
    const tagsOptions = tags.toOptions(
        (x) => x.name,
        (x) => x.id
    );
    const sourceAccountsOptions = filterSourceAccounts(accounts, destinationAccountType)
        .groupBy((x) => x.type)
        .toList()
        .toOptions(
            (x) => x + " accounts",
            (x) =>
                x.toOptions(
                    (y) => y.name,
                    (y) => y.id
                )
        );
    const destinationAccountsOptions = filterDestinationAccounts(accounts, sourceAccountType)
        .groupBy((x) => x.type)
        .toList()
        .toOptions(
            (x) => x + " accounts",
            (x) =>
                x.toOptions(
                    (y) => y.name,
                    (y) => y.id
                )
        );

    const transactionType = getTransactionType(sourceAccountType, destinationAccountType);

    if (added) return <Redirect to="/transactions" />;
    if (transaction == null || loading) {
        return <Spinner />;
    }

    return (
        <div>
            <ContentHeader>
                <h1 className="h2">New Transaction</h1>
            </ContentHeader>
            <Form onSubmit={handleSubmit(onSubmit)}>
                <Form.Row>
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

                    <Form.Group as={Col}>
                        <Form.Label>Date</Form.Label>
                        <DateTimePicker name="date" isInvalid={!!errors.date} control={control} />
                        <Form.Control.Feedback type="invalid">
                            {errors.date && errors.date.message}
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
                            isLoading={accountsLoading}
                            control={control}
                            placeholder="Select source account"
                        />
                        <Form.Control.Feedback type="invalid">
                            {errors.sourceAccountId && errors.sourceAccountId.message}
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
                            isLoading={accountsLoading}
                            control={control}
                            placeholder="Select destination account"
                        />
                        <Form.Control.Feedback type="invalid">
                            {errors.destinationAccountId && errors.destinationAccountId.message}
                        </Form.Control.Feedback>
                    </Form.Group>

                    <Form.Group as={Col}>
                        <Form.Label>Amount</Form.Label>
                        <NumberInput name="amount" isInvalid={!!errors.amount} control={control} />
                        <Form.Control.Feedback type="invalid">
                            {errors.amount && errors.amount.message}
                        </Form.Control.Feedback>
                    </Form.Group>
                </Form.Row>

                <Form.Row>
                    <Col>
                        <Form.Group>
                            <Form.Label>Category</Form.Label>
                            <Select
                                name="categoryId"
                                isInvalid={!!errors.categoryId}
                                options={categoriesOptions}
                                onInputChange={onCategoriesInputChange}
                                isLoading={categoriesLoading}
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
                                isLoading={tagsLoading}
                                control={control}
                                placeholder="Select tags"
                            />
                            <Form.Control.Feedback type="invalid">
                                {errors.tagIds && errors.tagIds.message}
                            </Form.Control.Feedback>
                        </Form.Group>
                    </Col>

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
                </Form.Row>

                <Button type="submit" variant="primary" disabled={loading}>
                    Submit
                </Button>
            </Form>
        </div>
    );
}

TransactionsEdit.propTypes = {
    transaction: PropTypes.object,
    loading: PropTypes.bool,
    added: PropTypes.bool,
    categoriesLoading: PropTypes.bool,
    categories: PropTypes.array,
    allCategoriesLoaded: PropTypes.bool,
    tagsLoading: PropTypes.bool,
    tags: PropTypes.array,
    allTagsLoaded: PropTypes.bool,
    accountsLoading: PropTypes.bool,
    accounts: PropTypes.array,
    allAccountsLoaded: PropTypes.bool,
    onSubmitTransaction: PropTypes.func,
    onFetchTransaction: PropTypes.func,
    onFetchCategoriesOptions: PropTypes.func,
    onFetchTagsOptions: PropTypes.func,
    onFetchAccountsOptions: PropTypes.func,
};

const mapStateToProps = (state) => {
    return {
        transaction: state.transaction.transaction,
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
        onFetchTransaction: (id) => dispatch(actions.fetchTransaction(id)),
        onSubmitTransaction: (id, transactionData) =>
            dispatch(actions.updateTransaction(id, transactionData)),
        onFetchCategoriesOptions: (search) => dispatch(actions.fetchCategoriesOptions(search)),
        onFetchTagsOptions: (search) => dispatch(actions.fetchTagsOptions(search)),
        onFetchAccountsOptions: (search) => dispatch(actions.fetchAccountsOptions(search)),
    };
};

const filterSourceAccounts = (accounts, destinationAccountType) => {
    return accounts.filter(
        (x) =>
            x.type === AccountType.asset ||
            (destinationAccountType !== AccountType.expense && x.type === AccountType.revenue)
    );
};

const filterDestinationAccounts = (accounts, sourceAccountType) => {
    return accounts.filter(
        (x) =>
            x.type === AccountType.asset ||
            (sourceAccountType !== AccountType.revenue && x.type === AccountType.expense)
    );
};

const getTransactionType = (sourceAccountType, destinationAccountType) => {
    if (sourceAccountType === AccountType.asset)
        return destinationAccountType === AccountType.asset
            ? TransactionType.transfer
            : TransactionType.withdrawal;

    if (sourceAccountType === AccountType.revenue) return TransactionType.deposit;
    if (sourceAccountType === AccountType.initialBalance) return TransactionType.openingBalance;
    if (sourceAccountType === AccountType.reconciliation) return TransactionType.reconciliation;

    return TransactionType.unknown;
};

export default connect(mapStateToProps, mapDispatchToProps)(TransactionsEdit);
