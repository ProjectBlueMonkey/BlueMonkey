import * as azureMobileApps from 'azure-mobile-apps';

let table = azureMobileApps.table();
table.access = 'authenticated';
table.insert.access = 'authenticated';
table.read.access = 'authenticated';
table.update.access = 'authenticated';
table.delete.access = 'authenticated';

table.perUser = true;

table.dynamicSchema = false;
table.columns = {
    expenseId: "string",
    receiptUrl: "string",
    userId: "string"
};

module.exports = table;