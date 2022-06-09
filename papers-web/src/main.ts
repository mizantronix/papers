import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { LoginModule } from './app/app.login';

const platform = platformBrowserDynamic();
platform.bootstrapModule(LoginModule);