import React, { useEffect } from "react";
import { connect } from "react-redux";
import ContentHeader from "../../components/ContentHeader";
import * as actions from "../../store/actions/index";
import { useParams } from "react-router-dom";
import Spinner from "../../components/UI/Spinner/Spinner";
import PropTypes from "prop-types";

function TagDetails({ tag, loading, onFetchTag }) {
    const { id } = useParams();
    useEffect(() => {
        onFetchTag(id);
    }, [id]);

    if (tag == null || loading) {
        return <Spinner />;
    }

    return (
        <div>
            <ContentHeader>
                <h1 className="h2">Tag details</h1>
            </ContentHeader>
            {tag.name}
        </div>
    );
}

TagDetails.propTypes = {
    tag: PropTypes.bool,
    loading: PropTypes.bool,
    onFetchTag: PropTypes.func,
};

const mapStateToProps = (state) => {
    return {
        tag: state.tag.tag,
        loading: state.tag.loading,
    };
};

const mapDispatchToProps = (dispatch) => {
    return {
        onFetchTag: (id) => dispatch(actions.fetchTag(id)),
    };
};

export default connect(mapStateToProps, mapDispatchToProps)(TagDetails);
