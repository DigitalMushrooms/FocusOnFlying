import { Injectable } from '@angular/core';
import { MessageService } from 'primeng/api';

@Injectable({
    providedIn: 'root'
})
export class MessageToast {
    private key = 'messageToast';

    constructor(
        private messageService: MessageService
    ) { }

    success(detail: string): void {
        this.messageService.add({ key: this.key, severity: 'success', detail, life: 3000 });
    }

    warning(detail: string): void {
        this.messageService.add({ key: this.key, severity: 'warn', detail, life: 3000 });
    }

    error(detail: string): void {
        this.messageService.add({ key: this.key, severity: 'error', detail, life: 60000, closable: true });
    }
}
