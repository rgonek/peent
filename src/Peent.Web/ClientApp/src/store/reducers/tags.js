import * as actionTypes from '../actions/actionTypes';
import { updateObject } from '../../shared/utility';
import Noty from 'noty';

const initialState = {
    tags: [],
    loading: false
};

const addTagStart = ( state, action ) => {
    return updateObject( state, { loading: true } );
};

const addTagSuccess = ( state, action ) => {
    new Noty({
        type: 'success',
        text: 'Tag added'
    }).show();
    const newTag = updateObject( action.tagData, { id: action.tagId } );
    return updateObject( state, {
        loading: false,
        tags: state.tags.concat( newTag )
    } );
};

const addTagFail = ( state, action ) => {
    return updateObject( state, { loading: false } );
};

const reducer = ( state = initialState, action ) => {
    switch ( action.type ) {
        case actionTypes.ADD_TAG_START: return addTagStart( state, action );
        case actionTypes.ADD_TAG_SUCCESS: return addTagSuccess( state, action );
        case actionTypes.ADD_TAG_FAIL: return addTagFail( state, action );
        default: return state;
    }
};

export default reducer;