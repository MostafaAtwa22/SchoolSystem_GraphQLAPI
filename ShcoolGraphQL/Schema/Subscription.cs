using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using SchoolGraphQL.Entities.Dtos;

namespace ShcoolGraphQL.Schema
{
    public class Subscription
    {
        // student
        [Subscribe]
        public StudentDto StudentCreate([EventMessage] StudentDto student)
            => student;

        [SubscribeAndResolve]
        public async ValueTask<ISourceStream<StudentDto>> StudentUpdate(int studentId,
            [Service] ITopicEventReceiver topicEventReceiver,
            CancellationToken cancellationToken)
        {
            var topicName = $"{studentId}_{nameof(StudentUpdate)}";
            return await topicEventReceiver.SubscribeAsync<StudentDto>(topicName, cancellationToken);
        }

        [SubscribeAndResolve]
        public async ValueTask<ISourceStream<StudentDto>> StudentDelete(int studentId,
            [Service] ITopicEventReceiver topicEventReceiver,
            CancellationToken cancellationToken)
        {
            var topicName = $"{studentId}_{nameof(StudentDelete)}";
            return await topicEventReceiver.SubscribeAsync<StudentDto>(topicName, cancellationToken);
        }

        // department
        [Subscribe]
        public DepartmentDto DepartmentCreate([EventMessage] DepartmentDto department)
            => department;

        [SubscribeAndResolve]
        public async ValueTask<ISourceStream<DepartmentDto>> DepartmentUpdate(int DepartmentId,
            [Service] ITopicEventReceiver topicEventReceiver,
            CancellationToken cancellationToken)
        {
            var topicName = $"{DepartmentId}_{nameof(DepartmentUpdate)}";
            return await topicEventReceiver.SubscribeAsync<DepartmentDto>(topicName, cancellationToken);
        }

        [SubscribeAndResolve]
        public async ValueTask<ISourceStream<DepartmentDto>> DepartmentDelete(int departmentId,
            [Service] ITopicEventReceiver topicEventReceiver,
            CancellationToken cancellationToken)
        {
            var topicName = $"{departmentId}_{nameof(DepartmentDelete)}";
            return await topicEventReceiver.SubscribeAsync<DepartmentDto>(topicName, cancellationToken);
        }

        // course
        [Subscribe]
        public CourseDto CourseCreate([EventMessage] CourseDto course)
            => course;

        [SubscribeAndResolve]
        public async ValueTask<ISourceStream<CourseDto>> CourseUpdate(int courseId,
            [Service] ITopicEventReceiver topicEventReceiver,
            CancellationToken cancellationToken)
        {
            var topicName = $"{courseId}_{nameof(CourseUpdate)}";
            return await topicEventReceiver.SubscribeAsync<CourseDto>(topicName, cancellationToken);
        }

        [SubscribeAndResolve]
        public async ValueTask<ISourceStream<CourseDto>> CourseDelete(int courseId,
            [Service] ITopicEventReceiver topicEventReceiver,
            CancellationToken cancellationToken)
        {
            var topicName = $"{courseId}_{nameof(CourseDelete)}";
            return await topicEventReceiver.SubscribeAsync<CourseDto>(topicName, cancellationToken);
        }
    }
}
