namespace GradeBook {

    public class NamedObject {
        public string Name {
            get;
            set; // if has private - unavailable to set name after it was set in constructor
        }

        //private string name;//no need in new way of define setter getter //property that has setter getter (we can adjust the property)
    }

}
