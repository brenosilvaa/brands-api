export class Brand {
    id!: number;
    name!: string;
    owner!: string;
    description!: string;

    // Propriedades de visualização
    isRemoving?: boolean;
}