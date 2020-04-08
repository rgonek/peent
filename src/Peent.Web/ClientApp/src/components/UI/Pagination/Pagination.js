import React from "react";
import { usePagination } from "react-pagination-hook";
import BootstrapPagination from "react-bootstrap/Pagination";
import PropTypes from "prop-types";
import { Link, useRouteMatch, useLocation } from "react-router-dom";
import * as constants from "../../../shared/constants";

function Pagination({ pageCount, onPageChange, pageIndex, maxButtons }) {
    const { activePage, visiblePieces } = usePagination({
        initialPage: pageIndex,
        numberOfPages: pageCount,
        maxButtons: maxButtons,
    });
    const { url } = useRouteMatch();
    const search = useLocation().search;
    const query = new URLSearchParams(search);

    const pageChange = (pageNumber) => {
        query.setOrDelete(
            constants.QUERY_PARAMETER_PAGE,
            pageNumber === constants.DEFAULT_PAGE ? null : pageNumber
        );
        query.sort();

        return url + "?" + query.toString();
    };

    const PageLink = (key, pageNumber, isDisabled, text) => {
        const isActive = !text && pageNumber === activePage;
        text = text ?? pageNumber;
        const classNames = [
            "page-item",
            isActive ? "active" : "",
            isDisabled ? "disabled" : "",
        ].join(" ");
        return (
            <li
                key={key}
                className={classNames}
                {...(isActive && { "aria-current": "page" })}
                {...(isDisabled && { "aria-disabled": true })}
            >
                <Link
                    to={() => pageChange(pageNumber)}
                    onClick={() => onPageChange(pageNumber)}
                    className="page-link"
                >
                    {text} {isActive && <span className="sr-only">(current)</span>}
                </Link>
            </li>
        );
    };

    return (
        <ul className="pagination">
            {visiblePieces.map((visiblePiece, index) => {
                const { pageNumber, type, isDisabled } = visiblePiece;
                const key = `${type}-${index}`;

                switch (type) {
                    case "ellipsis":
                        return <BootstrapPagination.Ellipsis key={key} disabled={true} />;
                    case "page-number":
                        return PageLink(key, pageNumber, isDisabled);
                    default:
                        return PageLink(key, pageNumber, isDisabled, type === "next" ? ">" : "<");
                }
            })}
        </ul>
    );
}

Pagination.propTypes = {
    pageCount: PropTypes.number,
    pageIndex: PropTypes.number,
    maxButtons: PropTypes.number,
    onPageChange: PropTypes.func,
};
Pagination.defaultProps = {
    pageIndex: constants.DEFAULT_PAGE,
    maxButtons: 5,
};

export default Pagination;
