export const AccountType = Object.freeze({
  unknown: "unknown",
  asset: "asset",
  expense: "expense",
  revenue: "revenue",
  liabilities: "liabilities",
  initialBalance: "initialBalance",
  reconciliation: "reconciliation"
});

export const TransactionType = Object.freeze({
  unknown: "unknown",
  withdrawal: "withdrawal",
  deposit: "deposit",
  transfer: "transfer",
  openingBalance: "openingBalance",
  reconciliation: "reconciliation"
});
