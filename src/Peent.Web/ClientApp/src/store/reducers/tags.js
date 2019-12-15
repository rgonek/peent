import * as actionTypes from '../actions/actionTypes';
import { updateObject } from '../../shared/utility';
import toast from '../../services/toast'

const initialState = {
    tags: [],
    loading: false,
    pageCount: 0,
    rowCount: 0,
    tag: null
};

const addTagStart = ( state, action ) => {
    return updateObject( state, { loading: true } );
};

const addTagSuccess = ( state, action ) => {
    toast.success('Tag added');
    return updateObject( state, { loading: false } );
};

const addTagFail = ( state, action ) => {
    return updateObject( state, { loading: false } );
};

const updateTagStart = ( state, action ) => {
    return updateObject( state, { loading: true } );
};

const updateTagSuccess = ( state, action ) => {
    toast.success('Tag updated');
    return updateObject( state, {
        loading: false,
        tag: action.tagData
    } );
};

const updateTagFail = ( state, action ) => {
    return updateObject( state, { loading: false } );
};

const fetchTagsStart = ( state, action ) => {
    return updateObject( state, { loading: true } );
};

const fetchTagsSuccess = ( state, action ) => {
    return updateObject( state, {
        tags: action.tags,
        pageCount: action.pageCount,
        rowCount: action.rowCount,
        loading: false
    } );
};

const fetchTagsFail = ( state, action ) => {
    return updateObject( state, { loading: false } );
};

const fetchTagStart = ( state, action ) => {
    return updateObject( state, { loading: true } );
};

const fetchTagSuccess = ( state, action ) => {
    return updateObject( state, {
        tag: action.tag,
        loading: false
    } );
};

const fetchTagFail = ( state, action ) => {
    return updateObject( state, { loading: false } );
};

const reducer = ( state = initialState, action ) => {
    switch ( action.type ) {
        case actionTypes.ADD_TAG_START: return addTagStart( state, action );
        case actionTypes.ADD_TAG_SUCCESS: return addTagSuccess( state, action );
        case actionTypes.ADD_TAG_FAIL: return addTagFail( state, action );

        case actionTypes.UPDATE_TAG_START: return updateTagStart( state, action );
        case actionTypes.UPDATE_TAG_SUCCESS: return updateTagSuccess( state, action );
        case actionTypes.UPDATE_TAG_FAIL: return updateTagFail( state, action );

        case actionTypes.FETCH_TAGS_START: return fetchTagsStart( state, action );
        case actionTypes.FETCH_TAGS_SUCCESS: return fetchTagsSuccess( state, action );
        case actionTypes.FETCH_TAGS_FAIL: return fetchTagsFail( state, action );

        case actionTypes.FETCH_TAG_START: return fetchTagStart( state, action );
        case actionTypes.FETCH_TAG_SUCCESS: return fetchTagSuccess( state, action );
        case actionTypes.FETCH_TAG_FAIL: return fetchTagFail( state, action );
        default: return state;
    }
};

export default reducer;