import System.Environment
import Data.List
import Data.Ord

main :: IO ()
main = do
  args <- getArgs
  let moves = if not (null args) then args !! 1 else ""
      fave = if not (null moves) then head $ maximumBy (comparing length) (group $ sort moves) else 'V'
  putChar $ case fave of 'R' -> 'P'
                         'P' -> 'S'
                         'S' -> 'R'
                         'L' -> 'R'
                         'V' -> 'P'
                         _   -> 'V'