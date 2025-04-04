﻿namespace DesignPatterns.Specification;

// Composite specification: OR condition
public class OrSpecification<T> : ISpecification<T>
{
    private readonly ISpecification<T> _left;
    private readonly ISpecification<T> _right;

    public OrSpecification(ISpecification<T> left, ISpecification<T> right)
    {
        _left = left;
        _right = right;
    }

    public bool IsSatisfiedBy(T candidate)
    {
        return _left.IsSatisfiedBy(candidate) || _right.IsSatisfiedBy(candidate);
    }
}