public interface IStudentService
{

    List<Student> Get();
    Student Get(string id);
    Student create(Student student);
    void Update(string id, Student student);
    void Delete(string id);

}