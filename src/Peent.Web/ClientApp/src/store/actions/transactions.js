import * as actionTypes from './actionTypes';
import axios from '../../axios-peent';
import { convertEmptyStringsToNulls } from '../../shared/utility'
import * as _ from '../../shared/extensions';

export const addTransaction = ( transactionData ) => {
    return dispatch => {
        transactionData = convertEmptyStringsToNulls(transactionData);
        dispatch( addTransactionStart() );
        axios.post( '/transactions', transactionData )
            .then( response => {
                dispatch( addTransactionSuccess( response.data.name, transactionData ) );
            } )
            .catch( error => {
                dispatch( addTransactionFail( error ) );
            } );
    };
};

export const addTransactionStart = () => {
    return {
        type: actionTypes.ADD_TRANSACTION_START
    };
};

export const addTransactionSuccess = ( id, transactionData ) => {
    return {
        type: actionTypes.ADD_TRANSACTION_SUCCESS,
        transactionId: id,
        transactionData: transactionData
    };
};

export const addTransactionFail = ( error ) => {
    return {
        type: actionTypes.ADD_TRANSACTION_FAIL,
        error: error
    };
};

export const updateTransaction = ( id, transactionData ) => {
    return dispatch => {
        transactionData = convertEmptyStringsToNulls(transactionData);
        dispatch( updateTransactionStart() );
        axios.put( '/transactions/' + id, transactionData )
            .then( response => {
                dispatch( updateTransactionSuccess( transactionData ) );
            } )
            .catch( error => {
                dispatch( updateTransactionFail( error ) );
            } );
    };
};

export const updateTransactionStart = () => {
    return {
        type: actionTypes.UPDATE_TRANSACTION_START
    };
};

export const updateTransactionSuccess = ( transactionData ) => {
    return {
        type: actionTypes.UPDATE_TRANSACTION_SUCCESS,
        transactionData: transactionData
    };
};

export const updateTransactionFail = ( error ) => {
    return {
        type: actionTypes.UPDATE_TRANSACTION_FAIL,
        error: error
    };
};

export const deleteTransaction = ( id ) => {
    return dispatch => {
        dispatch( deleteTransactionStart() );
        axios.delete( '/transactions/' + id )
            .then( response => {
                dispatch( deleteTransactionSuccess() );
            } )
            .catch( error => {
                dispatch( deleteTransactionFail( error ) );
            } );
    };
};

export const deleteTransactionStart = () => {
    return {
        type: actionTypes.DELETE_TRANSACTION_START
    };
};

export const deleteTransactionSuccess = () => {
    return {
        type: actionTypes.DELETE_TRANSACTION_SUCCESS
    };
};

export const deleteTransactionFail = ( error ) => {
    return {
        type: actionTypes.DELETE_TRANSACTION_FAIL,
        error: error
    };
};

export const fetchTransactionsSuccess = ( transactions, pageCount, rowCount ) => {
    return {
        type: actionTypes.FETCH_TRANSACTIONS_SUCCESS,
        transactions: transactions,
        pageCount: pageCount,
        rowCount: rowCount
    };
};

export const fetchTransactionsFail = ( error ) => {
    return {
        type: actionTypes.FETCH_TRANSACTIONS_FAIL,
        error: error
    };
};

export const fetchTransactionsStart = () => {
    return {
        type: actionTypes.FETCH_TRANSACTIONS_START
    };
};

export const fetchTransactions = (pageIndex, pageSize, sortBy, filters) => {
    return dispatch => {
        dispatch(fetchTransactionsStart());

        const query = {
            pageIndex,
            pageSize,
            sort: sortBy.toSortModel(),
            filters: filters.toFilterModel()
        };
        
        axios.post('/transactions/GetAll', query)
            .then( res => {
                const fetchedTransactions = res.data.results.map(x => ({...x}));
                dispatch(fetchTransactionsSuccess(fetchedTransactions, res.data.pageCount, res.data.rowCount));
            } )
            .catch( err => {
                dispatch(fetchTransactionsFail(err));
            } );
    };
};

export const fetchTransactionSuccess = ( transaction ) => {
    return {
        type: actionTypes.FETCH_TRANSACTION_SUCCESS,
        transaction: transaction
    };
};

export const fetchTransactionFail = ( error ) => {
    return {
        type: actionTypes.FETCH_TRANSACTION_FAIL,
        error: error
    };
};

export const fetchTransactionStart = () => {
    return {
        type: actionTypes.FETCH_TRANSACTION_START
    };
};

export const fetchTransaction = (id) => {
    return dispatch => {
        dispatch(fetchTransactionStart());

        axios.get('/Transactions/' + id, {transformResponse: [].concat(
            axios.defaults.transformResponse
            )
          })
            .then( res => {
                dispatch(fetchTransactionSuccess(res.data));
            } )
            .catch( err => {
                dispatch(fetchTransactionFail(err));
            } );
    };
};