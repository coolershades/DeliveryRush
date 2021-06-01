namespace DeliveryRush
{
    public enum GameObjectType
    {
        /* RESTAURANT */
        McDonalds,
        
        /* DOWNTOWN */
        // Boutique1,
        Boutique,
        DodoPizza,
        AlmaMater,
        SkyScraper1,

        /* RESIDENTIAL */
        // Flats in residential area
        Flat1,
        Flat2,
        Flat3,
        Flat4,
        Flat5,
        
        ResBack1,
        ResBack2,
        ResBack3,
            
        // Convenience stores (e.g. 5ka)
        ConvStore1,
        ConvStore2,
        ConvStore3,

        /* POOR */
        PoorFlat1,
        
        PoorBack1,
        PoorBack2,
        PoorBack3,
        
        /* YARD */
        Yard1,
        Yard2,
        
        /* CROSSROAD */
        CrossRoad,
        
        /* OBSTACLES */
        Puddle,
        Pigeon,
        TrashCan,
        Fence,
        Animal,
        Car,
        Car1,
        
        /* MISC */
        EndTrigger,
        Scooter = 101,
        Courier = 102
    }
}