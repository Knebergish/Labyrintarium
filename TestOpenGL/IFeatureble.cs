using System.Collections.Generic;


namespace TestOpenGL
{
    interface IFeatureble
    {
        int this[Feature feature] { get; }
        double CurrentExperience { get; set; }
        int CurrentLevel { get; }
        double NextLevelExperience { get; }
        int FreeFeaturesPoints { get; }
        
        List<int> GetAllFeatures();
        void SetFeature(Feature feature, int value);
        bool AdditionFeature(Feature feature);

        event TEventDelegate<IFeatureble> ChangeFeaturesEvent;
    }
}
