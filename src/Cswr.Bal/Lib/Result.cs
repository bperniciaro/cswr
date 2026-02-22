namespace Cswr.Bal.Lib
{
    /// <summary>
    /// Represents the result of an operation.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Gets or sets a message associatefd with the result.
        /// </summary>
        public string Message
        {
            get;
            protected set;
        }

        = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether an operation was successful.
        /// </summary>
        public bool Success
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets a value indicating whether an operation was a failure.
        /// </summary>
        public bool Failure
        {
            get
            {
                return !this.Success;
            }
        }

        /// <summary>
        /// Handles failures which includes an error message but no exception.
        /// </summary>
        /// <param name="message">The message to include in failure result.</param>
        /// <returns>A <see cref="Result"/> containing information about the failure.</returns>
        public static Result Fail(string message) => new ()
        {
            Success = false,
            Message = message,
        };

        /// <summary>
        /// Handles failures for a particular type which includes an error message but no exception.
        /// </summary>
        /// <typeparam name="T1">The type associated with the Result.</typeparam>
        /// <param name="message">The message to include in failure result.</param>
        /// <returns>A <see cref="Result"/> containing information about the failure.</returns>
        public static Result<T1> Fail<T1>(string message) => new Result<T1>()
        {
            Success = false,
            Message = message,
        };

        /// <summary>
        /// Handles failures which includes an error message and an exception.
        /// </summary>
        /// <param name="message">The message to include in failure result.</param>
        /// <param name="e">The exception produced during the failure.</param>
        /// <returns>A <see cref="Result"/> containing information about the failure.</returns>
        public static Result Fail(string message, Exception e) => new Result
        {
            Success = false,
            Message = message + " " + e.Message,
        };

        /// <summary>
        /// Handles failures for a particular type which includes an error message and an exception.
        /// </summary>
        /// <typeparam name="T1">The type associated with the Result.</typeparam>
        /// <param name="message">The message to include in failure result.</param>
        /// <param name="e">The exception produced during the failure.</param>
        /// <returns>A <see cref="Result"/> containing information about the failure.</returns>
        public static Result<T1> Fail<T1>(string message, Exception e) => new Result<T1>
        {
            Success = false,
            Message = message + " " + e.Message,
        };

        /// <summary>
        /// Handles failures which includes an exception.
        /// </summary>
        /// <param name="e">The exception produced during the failure.</param>
        /// <returns>A <see cref="Result"/> containing information about the failure.</returns>
        public static Result Fail(Exception e) => new ()
        {
            Success = false,
            Message = e.Message,
        };

        /// <summary>
        /// Handles failures for a particular type which includes an exception.
        /// </summary>
        /// <typeparam name="T1">The type associated with the Result.</typeparam>
        /// <param name="e">The exception produced during the failure.</param>
        /// <returns>A <see cref="Result"/> containing information about the failure.</returns>
        public static Result<T1> Fail<T1>(Exception e) => new ()
        {
            Success = false,
            Message = e.Message,
        };

        /// <summary>
        /// A method that signifies a successful operation.
        /// </summary>
        /// <param name="message">The message associated with the successful operation.</param>
        /// <returns>A <see cref="Result"/> containing information about the success.</returns>
        public static Result Ok(string message = "") => new ()
        {
            Success = true,
            Message = message,
        };

        /// <summary>
        /// A method that signifies a successful operation associated with a particular type.
        /// </summary>
        /// <typeparam name="T1">The type associated with the operation.</typeparam>
        /// <param name="value">The type associated with the result.</param>
        /// <param name="message">The message associated with the successful operation.</param>
        /// <returns>A <see cref="Result"/> containing information about the success.</returns>
        public static Result<T1> Ok<T1>(T1 value, string message = "") => new (value)
        {
            Message = message,
        };
    }

    /// <summary>
    /// Represents the result of an operation on a particular type.
    /// </summary>
    /// <typeparam name="T">The type associated with the operation.</typeparam>
    public class Result<T> : Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result{T}"/> class.
        /// </summary>
        public Result()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{T}"/> class.
        /// </summary>
        /// <param name="value">The type associated with the result.</param>
        public Result(T value)
        {
            this.Value = value;
            this.Success = value != null;
        }

        /// <summary>
        /// Gets the object to be returned as part of the result.
        /// </summary>
        public T? Value
        {
            get;
        }

        /// <summary>
        /// Returns the object's default value on failure, otherwise returns the value.
        /// </summary>
        /// <returns>The object's default value on failure, otherwise the value.</returns>
        public T? ValueOrDefault() => this.ValueOrDefault(null, default);

        /// <summary>
        /// Returns the specified default value on failure, otherwise returns the value.
        /// </summary>
        /// <param name="defaultValue">The default value to return on failure.</param>
        /// <returns>The specified default value on failure, otherwise the value.</returns>
        public T? ValueOrDefault(T defaultValue) => this.ValueOrDefault(null, defaultValue);

        /// <summary>
        /// Specifies a callback method to return on failure, otherwise returns the value.
        /// </summary>
        /// <param name="onFailure">A callback method to call on failure.</param>
        /// <returns>A callback method on failure, otherwise the value.</returns>
        public T? ValueOrDefault(Action<Result<T>> onFailure) => this.ValueOrDefault(onFailure, default);

        /// <summary>
        /// Calls a delegate on failure and returns the specified default value, otherwise returns the value.
        /// </summary>
        /// <param name="onFailure">The delegate to call on failure.</param>
        /// <param name="defaultValue">The default value to return on failure.</param>
        /// <returns>The specified default value on failure, otherwise the value.</returns>
        public T? ValueOrDefault(Action<Result<T>>? onFailure, T? defaultValue)
        {
            if (!this.Success)
            {
                onFailure?.Invoke(this);
                return defaultValue;
            }

            return this.Value;
        }
    }
}