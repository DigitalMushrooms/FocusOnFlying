import { LocaleSettings } from 'primeng/calendar';

export class Kalendarz {
    static en: LocaleSettings = {
        firstDayOfWeek: 0,
        dayNames: ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"],
        dayNamesShort: ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"],
        dayNamesMin: ["Su","Mo","Tu","We","Th","Fr","Sa"],
        monthNames: [ "January","February","March","April","May","June","July","August","September","October","November","December" ],
        monthNamesShort: [ "Jan", "Feb", "Mar", "Apr", "May", "Jun","Jul", "Aug", "Sep", "Oct", "Nov", "Dec" ],
        today: 'Today',
        clear: 'Clear',
        dateFormat: 'mm/dd/yy',
        weekHeader: 'Wk'
    };
    static pl: LocaleSettings = {
        firstDayOfWeek: 1,
        dayNames: ["niedziela", "poniedziałek", "wtorek", "środa", "czwartek", "piątek", "sobota"],
        dayNamesShort: ["niedz.", "pon.", "wt.", "śr.", "czw.", "pt.", "sob."],
        dayNamesMin: ["n","p","w","ś","c","pt","s"],
        monthNames: [ "styczeń","luty","marzec","kwiecień","maj","czerwiec","lipiec","sierpień","wrzesień","październik","listopad","grudzień" ],
        monthNamesShort: [ "st.", "lt.", "mrz.", "kw.", "mj.", "czrw.","lp.", "sp.", "wrz.", "prn.", "lst.", "gr." ],
        today: 'Dzisiaj',
        clear: 'Wyczyść',
        dateFormat: 'dd.mm.yy',
        weekHeader: 'Tydz.'
    };
}