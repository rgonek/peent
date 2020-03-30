import * as actionTypes from "../actions/actionTypes";
import { updateObject } from "../../shared/utility";
import toast from "../../services/toast";

const initialState = {
    accounts: [],
    loading: false,
    pageCount: 0,
    rowCount: 0,
    account: null,
    submitted: false,
};

const addAccountStart = (state) => {
    return updateObject(state, { loading: true });
};

const addAccountSuccess = (state) => {
    toast.success("Account added");
    return updateObject(state, { loading: false, submitted: true });
};

const addAccountFail = (state) => {
    return updateObject(state, { loading: false });
};

const updateAccountStart = (state) => {
    return updateObject(state, { loading: true });
};

const updateAccountSuccess = (state, action) => {
    toast.success("Account updated");
    return updateObject(state, {
        loading: false,
        account: action.accountData,
        submitted: true,
    });
};

const updateAccountFail = (state) => {
    return updateObject(state, { loading: false });
};

const deleteAccountStart = (state) => {
    return updateObject(state, { loading: true });
};

const deleteAccountSuccess = (state) => {
    toast.success("Account deleted");
    return updateObject(state, {
        loading: false,
        submitted: true,
    });
};

const deleteAccountFail = (state) => {
    return updateObject(state, { loading: false });
};

const fetchAccountsStart = (state) => {
    return updateObject(state, { loading: true, submitted: false });
};

const fetchAccountsSuccess = (state, action) => {
    return updateObject(state, {
        accounts: action.accounts,
        pageCount: action.pageCount,
        rowCount: action.rowCount,
        loading: false,
    });
};

const fetchAccountsFail = (state) => {
    return updateObject(state, { loading: false });
};

const fetchAccountsOptionsStart = (state) => {
    return updateObject(state, { loading: true, submitted: false });
};

const fetchAccountsOptionsSuccess = (state, action) => {
    return updateObject(state, {
        accounts: action.accounts,
        pageCount: action.pageCount,
        rowCount: action.rowCount,
        loading: false,
    });
};

const fetchAccountsOptionsFail = (state) => {
    return updateObject(state, { loading: false });
};

const fetchAccountStart = (state) => {
    return updateObject(state, { loading: true, submitted: false });
};

const fetchAccountSuccess = (state, action) => {
    return updateObject(state, {
        account: action.account,
        loading: false,
    });
};

const fetchAccountFail = (state) => {
    return updateObject(state, { loading: false });
};

const reducer = (state = initialState, action) => {
    switch (action.type) {
        case actionTypes.ADD_ACCOUNT_START:
            return addAccountStart(state, action);
        case actionTypes.ADD_ACCOUNT_SUCCESS:
            return addAccountSuccess(state, action);
        case actionTypes.ADD_ACCOUNT_FAIL:
            return addAccountFail(state, action);

        case actionTypes.UPDATE_ACCOUNT_START:
            return updateAccountStart(state, action);
        case actionTypes.UPDATE_ACCOUNT_SUCCESS:
            return updateAccountSuccess(state, action);
        case actionTypes.UPDATE_ACCOUNT_FAIL:
            return updateAccountFail(state, action);

        case actionTypes.DELETE_ACCOUNT_START:
            return deleteAccountStart(state, action);
        case actionTypes.DELETE_ACCOUNT_SUCCESS:
            return deleteAccountSuccess(state, action);
        case actionTypes.DELETE_ACCOUNT_FAIL:
            return deleteAccountFail(state, action);

        case actionTypes.FETCH_ACCOUNTS_START:
            return fetchAccountsStart(state, action);
        case actionTypes.FETCH_ACCOUNTS_SUCCESS:
            return fetchAccountsSuccess(state, action);
        case actionTypes.FETCH_ACCOUNTS_FAIL:
            return fetchAccountsFail(state, action);

        case actionTypes.FETCH_ACCOUNTS_OPTIONS_START:
            return fetchAccountsOptionsStart(state, action);
        case actionTypes.FETCH_ACCOUNTS_OPTIONS_SUCCESS:
            return fetchAccountsOptionsSuccess(state, action);
        case actionTypes.FETCH_ACCOUNTS_OPTIONS_FAIL:
            return fetchAccountsOptionsFail(state, action);

        case actionTypes.FETCH_ACCOUNT_START:
            return fetchAccountStart(state, action);
        case actionTypes.FETCH_ACCOUNT_SUCCESS:
            return fetchAccountSuccess(state, action);
        case actionTypes.FETCH_ACCOUNT_FAIL:
            return fetchAccountFail(state, action);
        default:
            return state;
    }
};

export default reducer;
