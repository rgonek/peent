import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import ContentHeader from '../../components/ContentHeader';
import * as actions from '../../store/actions/index';
import { useParams } from "react-router-dom";
import Spinner from '../../components/UI/Spinner/Spinner'

function AccountDetails(props) {
    const { id } = useParams();
    useEffect(() => {
        props.onFetchAccount(id);
    }, [id]);

    if(props.account == null || props.loading) {
        return <Spinner />
    }

    return (
        <div>
            <ContentHeader>
                <h1 className="h2">Account details</h1>
            </ContentHeader>
            {props.account.name}
        </div>
    );
}

const mapStateToProps = state => {
    return {
      account: state.account.account,
      loading: state.account.loading
    };
  };

const mapDispatchToProps = dispatch => {
  return {
    onFetchAccount: (id) => dispatch( actions.fetchAccount(id) )
  };
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
  )(AccountDetails);