export interface ClientOrder {
    OrderId: number;         
    RequiredDate: Date | null; 
    ShippedDate: Date | null;  
    ShipName: string;         
    ShipAddress: string;      
    ShipCity: string;         
  }
  