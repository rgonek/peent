import * as actionTypes from './actionTypes';
import axios from '../../axios-peent';
import { convertEmptyStringsToNulls } from '../../shared/utility'

export const addCategory = ( categoryData ) => {
    return dispatch => {
        categoryData = convertEmptyStringsToNulls(categoryData);
        dispatch( addCategoryStart() );
        axios.post( '/categories', categoryData )
            .then( response => {
                dispatch( addCategorySuccess( response.data.name, categoryData ) );
            } )
            .catch( error => {
                dispatch( addCategoryFail( error ) );
            } );
    };
};

export const addCategoryStart = () => {
    return {
        type: actionTypes.ADD_CATEGORY_START
    };
};

export const addCategorySuccess = ( id, categoryData ) => {
    return {
        type: actionTypes.ADD_CATEGORY_SUCCESS,
        categoryId: id,
        categoryData: categoryData
    };
};

export const addCategoryFail = ( error ) => {
    return {
        type: actionTypes.ADD_CATEGORY_FAIL,
        error: error
    };
};

export const updateCategory = ( id, categoryData ) => {
    return dispatch => {
        categoryData = convertEmptyStringsToNulls(categoryData);
        dispatch( updateCategoryStart() );
        axios.put( '/categories/' + id, categoryData )
            .then( response => {
                dispatch( updateCategorySuccess( categoryData ) );
            } )
            .catch( error => {
                dispatch( updateCategoryFail( error ) );
            } );
    };
};

export const updateCategoryStart = () => {
    return {
        type: actionTypes.UPDATE_CATEGORY_START
    };
};

export const updateCategorySuccess = ( categoryData ) => {
    return {
        type: actionTypes.UPDATE_CATEGORY_SUCCESS,
        categoryData: categoryData
    };
};

export const updateCategoryFail = ( error ) => {
    return {
        type: actionTypes.UPDATE_CATEGORY_FAIL,
        error: error
    };
};

export const deleteCategory = ( id ) => {
    return dispatch => {
        dispatch( deleteCategoryStart() );
        axios.delete( '/categories/' + id )
            .then( response => {
                dispatch( deleteCategorySuccess() );
            } )
            .catch( error => {
                dispatch( deleteCategoryFail( error ) );
            } );
    };
};

export const deleteCategoryStart = () => {
    return {
        type: actionTypes.DELETE_CATEGORY_START
    };
};

export const deleteCategorySuccess = () => {
    return {
        type: actionTypes.DELETE_CATEGORY_SUCCESS
    };
};

export const deleteCategoryFail = ( error ) => {
    return {
        type: actionTypes.DELETE_CATEGORY_FAIL,
        error: error
    };
};

export const fetchCategoriesSuccess = ( categories, pageCount, rowCount ) => {
    return {
        type: actionTypes.FETCH_CATEGORIES_SUCCESS,
        categories: categories,
        pageCount: pageCount,
        rowCount: rowCount
    };
};

export const fetchCategoriesFail = ( error ) => {
    return {
        type: actionTypes.FETCH_CATEGORIES_FAIL,
        error: error
    };
};

export const fetchCategoriesStart = () => {
    return {
        type: actionTypes.FETCH_CATEGORIES_START
    };
};

export const fetchCategories = (pageIndex, pageSize, sortBy, filters) => {
    return dispatch => {
        dispatch(fetchCategoriesStart());
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
        
        axios.post('/categories/GetAll', query)
            .then( res => {
                const fetchedCategories = [];
                for ( let key in res.data.results ) {
                    fetchedCategories.push( {
                        ...res.data.results[key]
                    } );
                }
                dispatch(fetchCategoriesSuccess(fetchedCategories, res.data.pageCount, res.data.rowCount));
            } )
            .catch( err => {
                dispatch(fetchCategoriesFail(err));
            } );
    };
};

export const fetchCategoriesOptionsSuccess = ( categories, pageCount, rowCount ) => {
    return {
        type: actionTypes.FETCH_CATEGORIES_OPTIONS_SUCCESS,
        categories: categories,
        pageCount: pageCount,
        rowCount: rowCount
    };
};

export const fetchCategoriesOptionsFail = ( error ) => {
    return {
        type: actionTypes.FETCH_CATEGORIES_OPTIONS_FAIL,
        error: error
    };
};

export const fetchCategoriesOptionsStart = () => {
    return {
        type: actionTypes.FETCH_CATEGORIES_OPTIONS_START
    };
};

export const fetchCategoriesOptions = (search) => {
    return dispatch => {
        dispatch(fetchCategoriesOptionsStart());
        const query = {
            pageIndex: 1,
            pageSize: 100,
            filters: [{
                field: '_',
                values: [search]
            }]
        };
        
        axios.post('/categories/GetAll', query)
            .then( res => {
                const fetchedCategories = [];
                for ( let key in res.data.results ) {
                    fetchedCategories.push( {
                        ...res.data.results[key]
                    } );
                }
                dispatch(fetchCategoriesOptionsSuccess(fetchedCategories, res.data.pageCount, res.data.rowCount));
            } )
            .catch( err => {
                dispatch(fetchCategoriesOptionsFail(err));
            } );
    };
};

export const fetchCategorySuccess = ( category ) => {
    return {
        type: actionTypes.FETCH_CATEGORY_SUCCESS,
        category: category
    };
};

export const fetchCategoryFail = ( error ) => {
    return {
        type: actionTypes.FETCH_CATEGORY_FAIL,
        error: error
    };
};

export const fetchCategoryStart = () => {
    return {
        type: actionTypes.FETCH_CATEGORY_START
    };
};

export const fetchCategory = (id) => {
    return dispatch => {
        dispatch(fetchCategoryStart());

        axios.get('/categories/' + id, {transformResponse: [].concat(
            axios.defaults.transformResponse
            )
          })
            .then( res => {
                dispatch(fetchCategorySuccess(res.data));
            } )
            .catch( err => {
                dispatch(fetchCategoryFail(err));
            } );
    };
};