import * as actionTypes from "../actions/actionTypes";
import { updateObject } from "../../shared/utility";
import toast from "../../services/toast";

const initialState = {
    categories: [],
    loading: false,
    pageCount: 0,
    rowCount: 0,
    category: null,
    submitted: false,
};

const addCategoryStart = (state) => {
    return updateObject(state, { loading: true });
};

const addCategorySuccess = (state) => {
    toast.success("Category added");
    return updateObject(state, { loading: false, submitted: true });
};

const addCategoryFail = (state) => {
    return updateObject(state, { loading: false });
};

const updateCategoryStart = (state) => {
    return updateObject(state, { loading: true });
};

const updateCategorySuccess = (state, action) => {
    toast.success("Category updated");
    return updateObject(state, {
        loading: false,
        category: action.categoryData,
        submitted: true,
    });
};

const updateCategoryFail = (state) => {
    return updateObject(state, { loading: false });
};

const deleteCategoryStart = (state) => {
    return updateObject(state, { loading: true });
};

const deleteCategorySuccess = (state) => {
    toast.success("Category deleted");
    return updateObject(state, {
        loading: false,
        submitted: true,
    });
};

const deleteCategoryFail = (state) => {
    return updateObject(state, { loading: false });
};

const fetchCategoriesStart = (state) => {
    return updateObject(state, { loading: true, submitted: false });
};

const fetchCategoriesSuccess = (state, action) => {
    return updateObject(state, {
        categories: action.categories,
        pageCount: action.pageCount,
        rowCount: action.rowCount,
        loading: false,
    });
};

const fetchCategoriesFail = (state) => {
    return updateObject(state, { loading: false });
};

const fetchCategoriesOptionsStart = (state) => {
    return updateObject(state, { loading: true, submitted: false });
};

const fetchCategoriesOptionsSuccess = (state, action) => {
    return updateObject(state, {
        categories: action.categories,
        pageCount: action.pageCount,
        rowCount: action.rowCount,
        loading: false,
    });
};

const fetchCategoriesOptionsFail = (state) => {
    return updateObject(state, { loading: false });
};

const fetchCategoryStart = (state) => {
    return updateObject(state, { loading: true, submitted: false });
};

const fetchCategorySuccess = (state, action) => {
    return updateObject(state, {
        category: action.category,
        loading: false,
    });
};

const fetchCategoryFail = (state) => {
    return updateObject(state, { loading: false });
};

const reducer = (state = initialState, action) => {
    switch (action.type) {
        case actionTypes.ADD_CATEGORY_START:
            return addCategoryStart(state, action);
        case actionTypes.ADD_CATEGORY_SUCCESS:
            return addCategorySuccess(state, action);
        case actionTypes.ADD_CATEGORY_FAIL:
            return addCategoryFail(state, action);

        case actionTypes.UPDATE_CATEGORY_START:
            return updateCategoryStart(state, action);
        case actionTypes.UPDATE_CATEGORY_SUCCESS:
            return updateCategorySuccess(state, action);
        case actionTypes.UPDATE_CATEGORY_FAIL:
            return updateCategoryFail(state, action);

        case actionTypes.DELETE_CATEGORY_START:
            return deleteCategoryStart(state, action);
        case actionTypes.DELETE_CATEGORY_SUCCESS:
            return deleteCategorySuccess(state, action);
        case actionTypes.DELETE_CATEGORY_FAIL:
            return deleteCategoryFail(state, action);

        case actionTypes.FETCH_CATEGORIES_START:
            return fetchCategoriesStart(state, action);
        case actionTypes.FETCH_CATEGORIES_SUCCESS:
            return fetchCategoriesSuccess(state, action);
        case actionTypes.FETCH_CATEGORIES_FAIL:
            return fetchCategoriesFail(state, action);

        case actionTypes.FETCH_CATEGORIES_OPTIONS_START:
            return fetchCategoriesOptionsStart(state, action);
        case actionTypes.FETCH_CATEGORIES_OPTIONS_SUCCESS:
            return fetchCategoriesOptionsSuccess(state, action);
        case actionTypes.FETCH_CATEGORIES_OPTIONS_FAIL:
            return fetchCategoriesOptionsFail(state, action);

        case actionTypes.FETCH_CATEGORY_START:
            return fetchCategoryStart(state, action);
        case actionTypes.FETCH_CATEGORY_SUCCESS:
            return fetchCategorySuccess(state, action);
        case actionTypes.FETCH_CATEGORY_FAIL:
            return fetchCategoryFail(state, action);
        default:
            return state;
    }
};

export default reducer;
