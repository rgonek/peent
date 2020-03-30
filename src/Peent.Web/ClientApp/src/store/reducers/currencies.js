import * as actionTypes from "../actions/actionTypes";
import { updateObject } from "../../shared/utility";

const initialState = {
    currencies: [],
    loading: false,
};

const fetchCurrenciesStart = (state) => {
    return updateObject(state, { loading: true });
};

const fetchCurrenciesSuccess = (state, action) => {
    return updateObject(state, {
        currencies: action.currencies,
        loading: false,
    });
};

const fetchCurrenciesFail = (state) => {
    return updateObject(state, { loading: false });
};

const reducer = (state = initialState, action) => {
    switch (action.type) {
        case actionTypes.FETCH_CURRENCIES_START:
            return fetchCurrenciesStart(state, action);
        case actionTypes.FETCH_CURRENCIES_SUCCESS:
            return fetchCurrenciesSuccess(state, action);
        case actionTypes.FETCH_CURRENCIES_FAIL:
            return fetchCurrenciesFail(state, action);
        default:
            return state;
    }
};

export default reducer;
