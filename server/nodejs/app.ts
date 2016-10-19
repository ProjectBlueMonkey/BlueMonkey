import * as express from 'express';
import * as azureMobileApps from 'azure-mobile-apps';

let app = express();
let mobile = azureMobileApps();

mobile.tables.import('./tables');
mobile.tables.initialize()
    .then(() =>
    {
        app.use(mobile);
        app.listen(process.env.PORT || 3000);
    });
