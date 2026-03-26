namespace WarrantyRepairCenter.Authentication
{
    class UnauthenticatedException : Exception
    {
        public UnauthenticatedException() { }

        public UnauthenticatedException(string message) : base(message) { }
    }
}
