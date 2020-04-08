export const AccountType = Object.freeze({
    unknown: "unknown",
    asset: "asset",
    expense: "expense",
    revenue: "revenue",
    liabilities: "liabilities",
    initialBalance: "initialBalance",
    reconciliation: "reconciliation",
});

export const TransactionType = Object.freeze({
    unknown: "unknown",
    withdrawal: "withdrawal",
    deposit: "deposit",
    transfer: "transfer",
    openingBalance: "openingBalance",
    reconciliation: "reconciliation",
});

export const SortDirection = Object.freeze({
    descending: "desc",
    ascending: "asc",
});

export const DEFAULT_PAGE = 1;
export const DEFAULT_PAGE_SIZE = 10;
export const SORT_DESCENDING_PREFIX = "-";
export const SORT_ASCENDING_PREFIX = "";
export const SORT_FIELDS_SEPARATOR = ",";
export const SORT_FIELDS_SEPARATOR_ENCODED = "%2C";
export const FILTER_VALUES_SEPARATOR = ",";
export const QUERY_PARAMETER_GLOBAL_FILTER = "q";
export const QUERY_PARAMETER_PAGE = "page";
export const QUERY_PARAMETER_PAGE_SIZE = "page_size";
export const QUERY_PARAMETER_SORT = "sort";
