import React, { useEffect } from "react";
import { connect } from "react-redux";
import ContentHeader from "../../components/ContentHeader";
import * as actions from "../../store/actions/index";
import { useParams } from "react-router-dom";
import Spinner from "../../components/UI/Spinner/Spinner";
import PropTypes from "prop-types";

function CategoryDetails({ category, loading, onFetchCategory }) {
    const { id } = useParams();
    useEffect(() => {
        onFetchCategory(id);
    }, [id, onFetchCategory]);

    if (category == null || loading) {
        return <Spinner />;
    }

    return (
        <div>
            <ContentHeader>
                <h1 className="h2">Category details</h1>
            </ContentHeader>
            {category.name}
        </div>
    );
}

CategoryDetails.propTypes = {
    category: PropTypes.object,
    loading: PropTypes.bool,
    onFetchCategory: PropTypes.func,
};

const mapStateToProps = (state) => {
    return {
        category: state.category.category,
        loading: state.category.loading,
    };
};

const mapDispatchToProps = (dispatch) => {
    return {
        onFetchCategory: (id) => dispatch(actions.fetchCategory(id)),
    };
};

export default connect(mapStateToProps, mapDispatchToProps)(CategoryDetails);
