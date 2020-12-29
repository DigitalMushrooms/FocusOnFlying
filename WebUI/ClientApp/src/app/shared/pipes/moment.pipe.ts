import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment';

@Pipe({ name: 'dateFormat' })
export class MomentPipe implements PipeTransform {
    transform(value: Date | moment.Moment, dateFormat: string): string {
        if (value) {
            return moment(value).format(dateFormat);
        } else {
            return 'nd.'
        }
    }
}