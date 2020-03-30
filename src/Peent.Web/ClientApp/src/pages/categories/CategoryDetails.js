import React, { useEffect } from "react";
import { connect } from "react-redux";
import ContentHeader from "../../components/ContentHeader";
import * as actions from "../../store/actions/index";
import { useParams } from "react-router-dom";
import Spinner from "../../components/UI/Spinner/Spinner";

function CategoryDetails(props) {
    const { id } = useParams();
    useEffect(() => {
        props.onFetchCategory(id);
    }, [id]);

    if (props.category == null || props.loading) {
        return <Spinner />;
    }

    return (
        <div>
            <ContentHeader>
                <h1 className="h2">Category details</h1>
            </ContentHeader>
            {props.category.name}
        </div>
    );
}

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
