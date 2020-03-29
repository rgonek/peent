import * as actionTypes from "../actions/actionTypes";
import { updateObject } from "../../shared/utility";
import toast from "../../services/toast";

const initialState = {
    tags: [],
    loading: false,
    pageCount: 0,
    rowCount: 0,
    tag: null,
    submitted: false,
};

const addTagStart = (state, action) => {
    return updateObject(state, { loading: true });
};

const addTagSuccess = (state, action) => {
    toast.success("Tag added");
    return updateObject(state, { loading: false, submitted: true });
};

const addTagFail = (state, action) => {
    return updateObject(state, { loading: false });
};

const updateTagStart = (state, action) => {
    return updateObject(state, { loading: true });
};

const updateTagSuccess = (state, action) => {
    toast.success("Tag updated");
    return updateObject(state, {
        loading: false,
        tag: action.tagData,
        submitted: true,
    });
};

const updateTagFail = (state, action) => {
    return updateObject(state, { loading: false });
};

const deleteTagStart = (state, action) => {
    return updateObject(state, { loading: true });
};

const deleteTagSuccess = (state, action) => {
    toast.success("Tag deleted");
    return updateObject(state, {
        loading: false,
        submitted: true,
    });
};

const deleteTagFail = (state, action) => {
    return updateObject(state, { loading: false });
};

const fetchTagsStart = (state, action) => {
    return updateObject(state, { loading: true, submitted: false });
};

const fetchTagsSuccess = (state, action) => {
    return updateObject(state, {
        tags: action.tags,
        pageCount: action.pageCount,
        rowCount: action.rowCount,
        loading: false,
    });
};

const fetchTagsFail = (state, action) => {
    return updateObject(state, { loading: false });
};

const fetchTagsOptionsStart = (state, action) => {
    return updateObject(state, { loading: true, submitted: false });
};

const fetchTagsOptionsSuccess = (state, action) => {
    return updateObject(state, {
        tags: action.tags,
        pageCount: action.pageCount,
        rowCount: action.rowCount,
        loading: false,
    });
};

const fetchTagsOptionsFail = (state, action) => {
    return updateObject(state, { loading: false });
};

const fetchTagStart = (state, action) => {
    return updateObject(state, { loading: true, submitted: false });
};

const fetchTagSuccess = (state, action) => {
    return updateObject(state, {
        tag: action.tag,
        loading: false,
    });
};

const fetchTagFail = (state, action) => {
    return updateObject(state, { loading: false });
};

const reducer = (state = initialState, action) => {
    switch (action.type) {
        case actionTypes.ADD_TAG_START:
            return addTagStart(state, action);
        case actionTypes.ADD_TAG_SUCCESS:
            return addTagSuccess(state, action);
        case actionTypes.ADD_TAG_FAIL:
            return addTagFail(state, action);

        case actionTypes.UPDATE_TAG_START:
            return updateTagStart(state, action);
        case actionTypes.UPDATE_TAG_SUCCESS:
            return updateTagSuccess(state, action);
        case actionTypes.UPDATE_TAG_FAIL:
            return updateTagFail(state, action);

        case actionTypes.DELETE_TAG_START:
            return deleteTagStart(state, action);
        case actionTypes.DELETE_TAG_SUCCESS:
            return deleteTagSuccess(state, action);
        case actionTypes.DELETE_TAG_FAIL:
            return deleteTagFail(state, action);

        case actionTypes.FETCH_TAGS_START:
            return fetchTagsStart(state, action);
        case actionTypes.FETCH_TAGS_SUCCESS:
            return fetchTagsSuccess(state, action);
        case actionTypes.FETCH_TAGS_FAIL:
            return fetchTagsFail(state, action);

        case actionTypes.FETCH_TAGS_OPTIONS_START:
            return fetchTagsOptionsStart(state, action);
        case actionTypes.FETCH_TAGS_OPTIONS_SUCCESS:
            return fetchTagsOptionsSuccess(state, action);
        case actionTypes.FETCH_TAGS_OPTIONS_FAIL:
            return fetchTagsOptionsFail(state, action);

        case actionTypes.FETCH_TAG_START:
            return fetchTagStart(state, action);
        case actionTypes.FETCH_TAG_SUCCESS:
            return fetchTagSuccess(state, action);
        case actionTypes.FETCH_TAG_FAIL:
            return fetchTagFail(state, action);
        default:
            return state;
    }
};

export default reducer;
