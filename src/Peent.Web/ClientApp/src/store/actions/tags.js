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
};

export const updateTag = ( id, tagData ) => {
    return dispatch => {
        tagData = convertEmptyStringsToNulls(tagData);
        dispatch( updateTagStart() );
        axios.put( '/tags/' + id, tagData )
            .then( response => {
                dispatch( updateTagSuccess( tagData ) );
            } )
            .catch( error => {
                dispatch( updateTagFail( error ) );
            } );
    };
};

export const updateTagStart = () => {
    return {
        type: actionTypes.UPDATE_TAG_START
    };
};

export const updateTagSuccess = ( tagData ) => {
    return {
        type: actionTypes.UPDATE_TAG_SUCCESS,
        tagData: tagData
    };
};

export const updateTagFail = ( error ) => {
    return {
        type: actionTypes.UPDATE_TAG_FAIL,
        error: error
    };
};

export const deleteTag = ( id ) => {
    return dispatch => {
        dispatch( deleteTagStart() );
        axios.delete( '/tags/' + id )
            .then( response => {
                dispatch( deleteTagSuccess() );
            } )
            .catch( error => {
                dispatch( deleteTagFail( error ) );
            } );
    };
};

export const deleteTagStart = () => {
    return {
        type: actionTypes.DELETE_TAG_START
    };
};

export const deleteTagSuccess = () => {
    return {
        type: actionTypes.DELETE_TAG_SUCCESS
    };
};

export const deleteTagFail = ( error ) => {
    return {
        type: actionTypes.DELETE_TAG_FAIL,
        error: error
    };
};

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
        const sortModel = sortBy.map((item, index) => {
            return {
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
                        ...res.data.results[key]
                    } );
                }
                dispatch(fetchTagsSuccess(fetchedTags, res.data.pageCount, res.data.rowCount));
            } )
            .catch( err => {
                dispatch(fetchTagsFail(err));
            } );
    };
};

export const fetchTagsOptionsSuccess = ( tags, pageCount, rowCount ) => {
    return {
        type: actionTypes.FETCH_TAGS_OPTIONS_SUCCESS,
        tags: tags,
        pageCount: pageCount,
        rowCount: rowCount
    };
};

export const fetchTagsOptionsFail = ( error ) => {
    return {
        type: actionTypes.FETCH_TAGS_OPTIONS_FAIL,
        error: error
    };
};

export const fetchTagsOptionsStart = () => {
    return {
        type: actionTypes.FETCH_TAGS_OPTIONS_START
    };
};

export const fetchTagsOptions = (search) => {
    return dispatch => {
        dispatch(fetchTagsOptionsStart());
        const filterModel = {
            field: "_",
            values: [search]
        };

        const query = {
            pageIndex: 1,
            pageSize: 100,
            filters: filterModel
        };
        
        axios.post('/tags/GetAll', query)
            .then( res => {
                const fetchedTags = [];
                for ( let key in res.data.results ) {
                    fetchedTags.push( {
                        ...res.data.results[key]
                    } );
                }
                dispatch(fetchTagsOptionsSuccess(fetchedTags, res.data.pageCount, res.data.rowCount));
            } )
            .catch( err => {
                dispatch(fetchTagsOptionsFail(err));
            } );
    };
};

export const fetchTagSuccess = ( tag ) => {
    return {
        type: actionTypes.FETCH_TAG_SUCCESS,
        tag: tag
    };
};

export const fetchTagFail = ( error ) => {
    return {
        type: actionTypes.FETCH_TAG_FAIL,
        error: error
    };
};

export const fetchTagStart = () => {
    return {
        type: actionTypes.FETCH_TAG_START
    };
};

export const fetchTag = (id) => {
    return dispatch => {
        dispatch(fetchTagStart());

        axios.get('/tags/' + id, {transformResponse: [].concat(
            axios.defaults.transformResponse
            )
          })
            .then( res => {
                dispatch(fetchTagSuccess(res.data));
            } )
            .catch( err => {
                dispatch(fetchTagFail(err));
            } );
    };
};