import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppComponent } from './app/app.component';
import { environment } from './environments/environment';

if (environment.production) {
    enableProdMode();
}

const platform = platformBrowserDynamic().bootstrapModule(AppComponent);

platform.then(success => console.log(`Bootstrap success`))
  .catch(err => console.error(err));