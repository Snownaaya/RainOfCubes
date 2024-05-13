public static class RandomGenerator
{
    private static System.Random s_random = new System.Random();

    public static int Range(int minimun, int maxium) => s_random.Next(minimun, maxium);
}
