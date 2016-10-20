"use strict";
var azureMobileApps = require('azure-mobile-apps');
var table = azureMobileApps.table();
table.access = 'authenticated';
table.insert.access = 'authenticated';
table.read.access = 'authenticated';
table.update.access = 'authenticated';
table.delete.access = 'authenticated';
table.perUser = true;
module.exports = table;
//# sourceMappingURL=report.js.map