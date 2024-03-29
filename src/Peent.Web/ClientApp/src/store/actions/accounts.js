import * as actionTypes from "./actionTypes";
import axios from "../../axios-peent";
import { convertEmptyStringsToNulls, buildQueryParams } from "../../shared/utility";
import * as constants from "../../shared/constants";

export const addAccount = (accountData) => {
    return (dispatch) => {
        accountData = convertEmptyStringsToNulls(accountData);
        dispatch(addAccountStart());
        axios
            .post("/accounts", accountData)
            .then((response) => {
                dispatch(addAccountSuccess(response.data.name, accountData));
            })
            .catch((error) => {
                dispatch(addAccountFail(error));
            });
    };
};

export const addAccountStart = () => {
    return {
        type: actionTypes.ADD_ACCOUNT_START,
    };
};

export const addAccountSuccess = (id, accountData) => {
    return {
        type: actionTypes.ADD_ACCOUNT_SUCCESS,
        accountId: id,
        accountData: accountData,
    };
};

export const addAccountFail = (error) => {
    return {
        type: actionTypes.ADD_ACCOUNT_FAIL,
        error: error,
    };
};

export const updateAccount = (id, accountData) => {
    return (dispatch) => {
        accountData = convertEmptyStringsToNulls(accountData);
        dispatch(updateAccountStart());
        axios
            .put("/accounts/" + id, accountData)
            .then(() => {
                dispatch(updateAccountSuccess(accountData));
            })
            .catch((error) => {
                dispatch(updateAccountFail(error));
            });
    };
};

export const updateAccountStart = () => {
    return {
        type: actionTypes.UPDATE_ACCOUNT_START,
    };
};

export const updateAccountSuccess = (accountData) => {
    return {
        type: actionTypes.UPDATE_ACCOUNT_SUCCESS,
        accountData: accountData,
    };
};

export const updateAccountFail = (error) => {
    return {
        type: actionTypes.UPDATE_ACCOUNT_FAIL,
        error: error,
    };
};

export const deleteAccount = (id) => {
    return (dispatch) => {
        dispatch(deleteAccountStart());
        axios
            .delete("/accounts/" + id)
            .then(() => {
                dispatch(deleteAccountSuccess());
            })
            .catch((error) => {
                dispatch(deleteAccountFail(error));
            });
    };
};

export const deleteAccountStart = () => {
    return {
        type: actionTypes.DELETE_ACCOUNT_START,
    };
};

export const deleteAccountSuccess = () => {
    return {
        type: actionTypes.DELETE_ACCOUNT_SUCCESS,
    };
};

export const deleteAccountFail = (error) => {
    return {
        type: actionTypes.DELETE_ACCOUNT_FAIL,
        error: error,
    };
};

export const fetchAccountsSuccess = (accounts, pageCount, rowCount) => {
    return {
        type: actionTypes.FETCH_ACCOUNTS_SUCCESS,
        accounts: accounts,
        pageCount: pageCount,
        rowCount: rowCount,
    };
};

export const fetchAccountsFail = (error) => {
    return {
        type: actionTypes.FETCH_ACCOUNTS_FAIL,
        error: error,
    };
};

export const fetchAccountsStart = () => {
    return {
        type: actionTypes.FETCH_ACCOUNTS_START,
    };
};

export const fetchAccounts = (pageIndex, pageSize, sortBy, filters) => {
    return (dispatch) => {
        dispatch(fetchAccountsStart());
        const query = buildQueryParams(pageIndex, pageSize, sortBy, filters);

        axios
            .get("/accounts", { params: { ...query } })
            .then((res) => {
                const fetchedAccounts = res.data.results.map((x) => ({ ...x }));
                dispatch(
                    fetchAccountsSuccess(fetchedAccounts, res.data.pageCount, res.data.rowCount)
                );
            })
            .catch((err) => {
                dispatch(fetchAccountsFail(err));
            });
    };
};

export const fetchAccountsOptionsSuccess = (accounts, pageCount, rowCount) => {
    return {
        type: actionTypes.FETCH_ACCOUNTS_OPTIONS_SUCCESS,
        accounts: accounts,
        pageCount: pageCount,
        rowCount: rowCount,
    };
};

export const fetchAccountsOptionsFail = (error) => {
    return {
        type: actionTypes.FETCH_ACCOUNTS_OPTIONS_FAIL,
        error: error,
    };
};

export const fetchAccountsOptionsStart = () => {
    return {
        type: actionTypes.FETCH_ACCOUNTS_OPTIONS_START,
    };
};

export const fetchAccountsOptions = (search) => {
    return (dispatch) => {
        dispatch(fetchAccountsOptionsStart());

        const filters = [{ id: constants.QUERY_PARAMETER_GLOBAL_FILTER, value: search }];
        const query = buildQueryParams(1, 500, [], filters);

        axios
            .get("/accounts", { params: { ...query } })
            .then((res) => {
                const fetchedAccounts = res.data.results.map((x) => ({ ...x }));
                dispatch(
                    fetchAccountsOptionsSuccess(
                        fetchedAccounts,
                        res.data.pageCount,
                        res.data.rowCount
                    )
                );
            })
            .catch((err) => {
                dispatch(fetchAccountsOptionsFail(err));
            });
    };
};

export const fetchAccountSuccess = (account) => {
    return {
        type: actionTypes.FETCH_ACCOUNT_SUCCESS,
        account: account,
    };
};

export const fetchAccountFail = (error) => {
    return {
        type: actionTypes.FETCH_ACCOUNT_FAIL,
        error: error,
    };
};

export const fetchAccountStart = () => {
    return {
        type: actionTypes.FETCH_ACCOUNT_START,
    };
};

export const fetchAccount = (id) => {
    return (dispatch) => {
        dispatch(fetchAccountStart());

        axios
            .get("/accounts/" + id, {
                transformResponse: [].concat(axios.defaults.transformResponse),
            })
            .then((res) => {
                dispatch(fetchAccountSuccess(res.data));
            })
            .catch((err) => {
                dispatch(fetchAccountFail(err));
            });
    };
};
