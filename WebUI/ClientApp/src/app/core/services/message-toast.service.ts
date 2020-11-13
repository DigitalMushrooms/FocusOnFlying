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

    public success(detail: string): void {
        this.messageService.add({ key: this.key, severity: 'success', detail, life: 3000 });
    }
}
