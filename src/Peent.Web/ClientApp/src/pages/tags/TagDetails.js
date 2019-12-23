import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import ContentHeader from '../../components/ContentHeader';
import * as actions from '../../store/actions/index';
import { useParams } from "react-router-dom";
import Spinner from '../../components/UI/Spinner/Spinner'

function TagDetails(props) {
    const { id } = useParams();
    useEffect(() => {
        props.onFetchTag(id);
    }, [id]);

    if(props.tag == null || props.loading) {
        return <Spinner />
    }

    return (
        <div>
            <ContentHeader>
                <h1 className="h2">Tag details</h1>
            </ContentHeader>
            {props.tag.name}
        </div>
    );
}

const mapStateToProps = state => {
    return {
      tag: state.tag.tag,
      loading: state.tag.loading
    };
  };

const mapDispatchToProps = dispatch => {
  return {
    onFetchTag: (id) => dispatch( actions.fetchTag(id) )
  };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
  )(TagDetails);