"use strict";
var express = require('express');
var azureMobileApps = require('azure-mobile-apps');
var app = express();
var mobile = azureMobileApps();
mobile.tables.import('./tables');
mobile.tables.initialize()
    .then(function () {
    app.use(mobile);
    app.listen(process.env.PORT || 3000);
});
//# sourceMappingURL=app.js.map