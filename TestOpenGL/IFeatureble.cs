using System.Collections.Generic;


namespace TestOpenGL
{
    interface IFeatureble
    {
        int this[Feature feature] { get; }
        double CurrentExperience { get; }
        int CurrentLevel { get; }
        double NextLevelExperience { get; }
        int FreeFeaturesPoints { get; }
        
        List<int> GetAllFeatures();
        void SetFeature(Feature feature, int value);
        bool AdditionFeature(Feature feature);
        void AddExperience(int value);

        event TEventDelegate<IFeatureble> ChangeFeaturesEvent;
    }
}
