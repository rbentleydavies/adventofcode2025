using Google.OrTools.LinearSolver;

public class LinearEquationSolver
{
    public static int[]? FindMinimumSumSolution(int[,] A, int[] b)
    {
        int numEquations = A.GetLength(0);
        int numVars = A.GetLength(1);
        
        // Create the linear solver with the SCIP backend (free, open-source)
        Solver solver = Solver.CreateSolver("SCIP");
        if (solver == null)
        {
            Console.WriteLine("Could not create solver SCIP");
            return null;
        }
        
        // Create variables (non-negative integers)
        Variable[] x = new Variable[numVars];
        for (int i = 0; i < numVars; i++)
        {
            // Upper bound: sum of all target values is a safe upper bound
            x[i] = solver.MakeIntVar(0.0, b.Sum(), $"x{i}");
        }
        
        // Add constraints: A * x = b
        for (int eq = 0; eq < numEquations; eq++)
        {
            Constraint constraint = solver.MakeConstraint(b[eq], b[eq], $"eq{eq}");
            for (int v = 0; v < numVars; v++)
            {
                constraint.SetCoefficient(x[v], A[eq, v]);
            }
        }
        
        // Objective: minimize sum of all variables
        Objective objective = solver.Objective();
        for (int i = 0; i < numVars; i++)
        {
            objective.SetCoefficient(x[i], 1);
        }
        objective.SetMinimization();
        
        // Solve
        Console.WriteLine("Solving with OR-Tools...");
        Solver.ResultStatus resultStatus = solver.Solve();
        
        // Check if solution was found
        if (resultStatus == Solver.ResultStatus.OPTIMAL || resultStatus == Solver.ResultStatus.FEASIBLE)
        {
            int[] solution = new int[numVars];
            for (int i = 0; i < numVars; i++)
            {
                solution[i] = (int)Math.Round(x[i].SolutionValue());
            }
            
            Console.WriteLine($"Solution found! Status: {resultStatus}");
            return solution;
        }
        
        Console.WriteLine($"No solution found. Status: {resultStatus}");
        return null;
    }
}
