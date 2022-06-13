import { Component } from '@angular/core';

@Component({
    selector: 'papers-web',
    template: `<label>Введите имя:</label>
               <papers-login></papers-login>
            <h1>Добро пожаловать {{name}}!</h1>`
})
export class AppComponent {
    name = '';
}
