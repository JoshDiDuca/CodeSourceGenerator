namespace CodeSourceGenerator.Models.Generations
{
    public class GenerationModel<T>
    {
        public GenerationModel(int generation, T model)
        {
            Generation = generation;
            Model = model;
        }
        public T Model { get; set; }
        public int Generation { get; set; }
    }
}
