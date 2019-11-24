import * as actionTypes from './actionTypes';
import axios from '../../axios-peent';
import { convertEmptyStringsToNulls } from '../../shared/utility'

export const addTag = ( tagData ) => {
    return dispatch => {
        console.log('addTag');
        tagData = convertEmptyStringsToNulls(tagData);
        dispatch( addTagStart() );
        axios.post( '/tags', tagData )
            .then( response => {
                dispatch( addTagSuccess( response.data.name, tagData ) );
            } )
            .catch( error => {
                dispatch( addTagFail( error ) );
            } );
    };
};

export const addTagStart = () => {
    return {
        type: actionTypes.ADD_TAG_START
    };
};

export const addTagSuccess = ( id, tagData ) => {
    return {
        type: actionTypes.ADD_TAG_SUCCESS,
        tagId: id,
        tagData: tagData
    };
};

export const addTagFail = ( error ) => {
    return {
        type: actionTypes.ADD_TAG_FAIL,
        error: error
    };
}