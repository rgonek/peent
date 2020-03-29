import * as actionTypes from "../actions/actionTypes";
import { updateObject } from "../../shared/utility";
import toast from "../../services/toast";

const initialState = {
    transactions: [],
    loading: false,
    pageCount: 0,
    rowCount: 0,
    tag: null,
    submitted: false,
};

const addTransactionStart = (state, action) => {
    return updateObject(state, { loading: true });
};

const addTransactionSuccess = (state, action) => {
    toast.success("Transaction added");
    return updateObject(state, { loading: false, submitted: true });
};

const addTransactionFail = (state, action) => {
    return updateObject(state, { loading: false });
};

const updateTransactionStart = (state, action) => {
    return updateObject(state, { loading: true });
};

const updateTransactionSuccess = (state, action) => {
    toast.success("Transaction updated");
    return updateObject(state, {
        loading: false,
        transaction: action.transactionData,
        submitted: true,
    });
};

const updateTransactionFail = (state, action) => {
    return updateObject(state, { loading: false });
};

const deleteTransactionStart = (state, action) => {
    return updateObject(state, { loading: true });
};

const deleteTransactionSuccess = (state, action) => {
    toast.success("Transaction deleted");
    return updateObject(state, {
        loading: false,
        submitted: true,
    });
};

const deleteTransactionFail = (state, action) => {
    return updateObject(state, { loading: false });
};

const fetchTransactionsStart = (state, action) => {
    return updateObject(state, { loading: true, submitted: false });
};

const fetchTransactionsSuccess = (state, action) => {
    return updateObject(state, {
        transactions: action.transactions,
        pageCount: action.pageCount,
        rowCount: action.rowCount,
        loading: false,
    });
};

const fetchTransactionsFail = (state, action) => {
    return updateObject(state, { loading: false });
};

const fetchTransactionStart = (state, action) => {
    return updateObject(state, { loading: true, submitted: false });
};

const fetchTransactionSuccess = (state, action) => {
    return updateObject(state, {
        transaction: action.transaction,
        loading: false,
    });
};

const fetchTransactionFail = (state, action) => {
    return updateObject(state, { loading: false });
};

const reducer = (state = initialState, action) => {
    switch (action.type) {
        case actionTypes.ADD_TRANSACTION_START:
            return addTransactionStart(state, action);
        case actionTypes.ADD_TRANSACTION_SUCCESS:
            return addTransactionSuccess(state, action);
        case actionTypes.ADD_TRANSACTION_FAIL:
            return addTransactionFail(state, action);

        case actionTypes.UPDATE_TRANSACTION_START:
            return updateTransactionStart(state, action);
        case actionTypes.UPDATE_TRANSACTION_SUCCESS:
            return updateTransactionSuccess(state, action);
        case actionTypes.UPDATE_TRANSACTION_FAIL:
            return updateTransactionFail(state, action);

        case actionTypes.DELETE_TRANSACTION_START:
            return deleteTransactionStart(state, action);
        case actionTypes.DELETE_TRANSACTION_SUCCESS:
            return deleteTransactionSuccess(state, action);
        case actionTypes.DELETE_TRANSACTION_FAIL:
            return deleteTransactionFail(state, action);

        case actionTypes.FETCH_TRANSACTIONS_START:
            return fetchTransactionsStart(state, action);
        case actionTypes.FETCH_TRANSACTIONS_SUCCESS:
            return fetchTransactionsSuccess(state, action);
        case actionTypes.FETCH_TRANSACTIONS_FAIL:
            return fetchTransactionsFail(state, action);

        case actionTypes.FETCH_TRANSACTION_START:
            return fetchTransactionStart(state, action);
        case actionTypes.FETCH_TRANSACTION_SUCCESS:
            return fetchTransactionSuccess(state, action);
        case actionTypes.FETCH_TRANSACTION_FAIL:
            return fetchTransactionFail(state, action);
        default:
            return state;
    }
};

export default reducer;
