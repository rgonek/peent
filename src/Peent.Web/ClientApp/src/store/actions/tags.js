import * as actionTypes from './actionTypes';
import axios from '../../axios-peent';
import { convertEmptyStringsToNulls } from '../../shared/utility'

export const addTag = ( tagData ) => {
    return dispatch => {
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

export const fetchTagsSuccess = ( tags, pageCount, rowCount ) => {
    return {
        type: actionTypes.FETCH_TAGS_SUCCESS,
        tags: tags,
        pageCount: pageCount,
        rowCount: rowCount
    };
};

export const fetchTagsFail = ( error ) => {
    return {
        type: actionTypes.FETCH_TAGS_FAIL,
        error: error
    };
};

export const fetchTagsStart = () => {
    return {
        type: actionTypes.FETCH_TAGS_START
    };
};

export const fetchTags = (pageIndex, pageSize) => {
    return dispatch => {
        dispatch(fetchTagsStart());
        const queryParams = '?pageSize=' + pageSize + '&pageIndex=' + pageIndex;
        axios.get('/tags/GetAll' + queryParams)
            .then( res => {
                const fetchedTags = [];
                for ( let key in res.data.results ) {
                    fetchedTags.push( {
                        ...res.data.results[key],
                        id: key
                    } );
                }
                dispatch(fetchTagsSuccess(fetchedTags, res.data.pageCount, res.data.rowCount));
            } )
            .catch( err => {
                dispatch(fetchTagsFail(err));
            } );
    };
};