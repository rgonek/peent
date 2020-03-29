import React from "react";
import { usePagination } from "react-pagination-hook";
import BootstrapPagination from "react-bootstrap/Pagination";

function Pagination({ pageCount, onPageChange, pageIndex = 1, maxButtons = 5 }) {
    const { activePage, visiblePieces } = usePagination({
        initialPage: pageIndex,
        numberOfPages: pageCount,
        maxButtons: maxButtons,
    });
    return (
        <BootstrapPagination>
            {visiblePieces.map((visiblePiece, index) => {
                const key = `${visiblePiece.type}-${index}`;

                if (visiblePiece.type === "ellipsis") {
                    return <BootstrapPagination.Ellipsis key={key} disabled={true} />;
                }

                const { pageNumber } = visiblePiece;

                if (visiblePiece.type === "page-number") {
                    const isActive = pageNumber === activePage;

                    return (
                        <BootstrapPagination.Item
                            key={key}
                            onClick={isActive ? null : () => onPageChange(pageNumber)}
                            active={isActive}
                        >
                            {pageNumber}
                        </BootstrapPagination.Item>
                    );
                }

                return (
                    <BootstrapPagination.Item
                        key={key}
                        onClick={() => onPageChange(pageNumber)}
                        disabled={visiblePiece.isDisabled}
                    >
                        {visiblePiece.type === "next" ? ">" : "<"}
                    </BootstrapPagination.Item>
                );
            })}
        </BootstrapPagination>
    );
}

export default Pagination;
