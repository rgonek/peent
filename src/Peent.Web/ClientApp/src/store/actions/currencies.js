import * as actionTypes from "./actionTypes";
import axios from "../../axios-peent";

export const fetchCurrenciesSuccess = (currencies) => {
    return {
        type: actionTypes.FETCH_CURRENCIES_SUCCESS,
        currencies: currencies,
    };
};

export const fetchCurrenciesFail = (error) => {
    return {
        type: actionTypes.FETCH_CURRENCIES_FAIL,
        error: error,
    };
};

export const fetchCurrenciesStart = () => {
    return {
        type: actionTypes.FETCH_CURRENCIES_START,
    };
};

export const fetchCurrencies = () => {
    return (dispatch) => {
        dispatch(fetchCurrenciesStart());

        axios
            .get("/currencies")
            .then((res) => {
                const fetchedCurrencies = res.data.results.map((x) => ({ ...x }));
                dispatch(fetchCurrenciesSuccess(fetchedCurrencies));
            })
            .catch((err) => {
                dispatch(fetchCurrenciesFail(err));
            });
    };
};
