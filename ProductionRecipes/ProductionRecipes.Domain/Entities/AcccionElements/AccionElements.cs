namespace ProductionRecipes.Domain.Entities.AccionElements
{
    public class AccionElement
    {
        #region Properties
        /// <summary>
        /// Nombre de la Operacion
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Descripcion de la operacion
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Genera la clase <see langword="base"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public AccionElement(string name, string description)
        {
            Name = name;
            Description = description;
        }
        #endregion
    }
}
