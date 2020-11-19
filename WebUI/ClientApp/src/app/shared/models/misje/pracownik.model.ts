import { Claim } from './claim.model';

export interface Pracownik {
    subjectId: string,
    username: string,
    isActive: boolean,
    claims: Claim[],
}