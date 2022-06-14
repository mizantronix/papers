import { Injectable } from '@angular/core';
// import { Observable } from 'rxjs';
// import { map } from 'rxjs/operators';

import { ApiService } from './api.service';
import { RegistrationData } from '../models'
import { SendResult } from '../enums/sendResult.enum'


@Injectable()
export class RegistrationService {
  constructor (
    private apiService: ApiService
  ) {}

  register(registrationData: RegistrationData): SendResult {
    return this.apiService
    .post('/users/register', { registrationData })
    .pipe();
  }
/*
  add(slug, payload): Observable<Comment> {
    return this.apiService
    .post(
      `/articles/${slug}/comments`,
      { comment: { body: payload } }
    ).pipe(map(data => data.comment));
  }
*/
}
