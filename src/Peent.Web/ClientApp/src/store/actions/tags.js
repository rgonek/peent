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

export const fetchTags = (pageIndex, pageSize, sortBy, filters) => {
    return dispatch => {
        dispatch(fetchTagsStart());
        const sortModel = [];
        sortBy.map((item, index) => {
            sortModel[index] = {
                field: item.id,
                direction: item.desc ? 'desc' : 'asc'
            };
        });
        const filterModel = Object.keys(filters)
            .map(key => {
                return {
                    field: key,
                    values: [filters[key]]
                }
            });

        const query = {
            pageIndex,
            pageSize,
            sort: sortModel,
            filters: filterModel
        };
        
        axios.post('/tags/GetAll', query)
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