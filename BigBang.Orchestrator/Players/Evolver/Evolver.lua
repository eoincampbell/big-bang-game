
-- The Evolver Bot
-- Vittorio "Robotik" Parrella - 01-08-2014

DEBUG = false
DATAFILE = "data.lua"

math.randomseed( os.time() )

my_moves = arg[1] or ""
opponent_moves = arg[2] or "-"

--[[
  Print debug information
]]
function dprint( ... )
    if DEBUG then
        print( "------" )
        print( ... )
    end
end

--[[
    This bot makes use (kind-of) an evolutionary algorithm, 
    modifying the ranges used to randomly select the next
    symbol to play.

    The fitness function for the data ranges checks if a 
    given set of ranges can create a sequence of symbols
    that is sufficiently good to beat the last 15 opponent plays,
    so that it can be used to actually select a symbol that 
    _could_ beat the next unkwown opponent symbol.
]]
function main()
    -- Initializes the bot.
    gen = new( genetic )

    -- Loads the data from the file or in
    -- case generates new data.
    if gen:load( DATAFILE ) == false then
        gen:generate( math.random( 5,10 ) )
    end

    -- Updates the fitness data with the new moves.
    gen:calculate_fitness( opponent_moves )

    -- Selects and prints the next symbol using the 
    -- distribution intervals with maximum fitness.
    print( gen:getSymbol( gen.data[1] , opponent_moves ) )

    dprint( gen:print() )

    -- Saves the calculated evolutionary data.
    gen:save( DATAFILE )
end

--[[ 
   Matrix for the rules:
   0 = draw
   1 = symbol 1 wins
   2 = symbol 2 wins
 ]]
SYMBOLS = { "R","P","S","L","V" }
rules = {
    ["R"] = { ["R"]=0,["P"]=2,["S"]=1,["L"]=1,["V"]=2 },
    ["P"] = { ["R"]=1,["P"]=0,["S"]=2,["L"]=2,["V"]=1 },
    ["S"] = { ["R"]=2,["P"]=1,["S"]=0,["L"]=1,["V"]=2 },
    ["L"] = { ["R"]=2,["P"]=1,["S"]=2,["L"]=0,["V"]=1 },
    ["V"] = { ["R"]=1,["P"]=2,["S"]=1,["L"]=2,["V"]=0 },
}

dprint( my_moves, opponent_moves )

function limit( min, max, value )
    return math.max( min, math.min( max, value ) )
end

--[[
   Very simple object orientation
]]
function new( class )
    local t = {}

    for i,k in pairs( class ) do
        t[i] = k
    end

    return t
end

--[[
   Class for the genetic algorithm
]]
genetic = {
    -- Prints the rows of paramters in the object + the fitness value for the combination
    print = function( self )
        local str = ""
        for i,fit in pairs( self.data ) do
            str = str.."{ "..table.concat(fit, ",", 1, 5).." } [".. fit[6] .."]\n"
        end
        return str
    end,

    --[[ 
      Writes the bots current data to the file in a format easily
      loadable with Lua.
    ]]
    save = function ( self, filepath )
        local file = io.open( filepath, "w" )
        assert( not(file == nil), "file cannot be opened for write: "..filepath )

        file:write( "return {\n" )
        for _,fit in pairs( self.data ) do
            file:write( "  { " )
            for j=1,#fit do
                file:write( fit[j]..", " )
            end
            file:write( "},\n" )
        end
        file:write( "}\n" )

        file:close()
    end,

    --[[
        Tries to load in memory the evolutionary parameters data table 
        from the data.lua file.
    ]]
    load = function( self, filepath )
        result, self.data = pcall( dofile, filepath )

        if result == false then
            self.data = nil
        end

        return result
    end,

    --[[
        Uses the eveolutionary data row (should always be the max fitness one) to 
        choose the symbol to print using a non-uniform distribution.
    ]]
    getSymbol = function( self, data )
        local roll = math.random()
        for case=1,5 do
            if roll < data[ case ] then
                return SYMBOLS[ case ]
            end
        end
    end,

    --[[
        Calculates the fitness value for all the data rows in the bot
        in respect to the last 15 moves of the opponent.
    ]]
    calculate_fitness = function( self, opp_moves )
        for i,row in pairs( self.data ) do
            local wins = 0
            local draws = 0

            --[[
                Here we basically check if every row would be capable of defeating
                the last 15 moves of the opponent, so that it can be used to MAYBE
                defeat it's next moves.
            ]]
            for piv=math.max(#opp_moves, 15),1,-1 do
                local sym = self:getSymbol( row ) -- A possible symbol calculated using this row
                local opp_sym = opp_moves:sub( piv,piv ) -- The next move used by the opponent

                local result = rules[ sym ][ opp_sym ]

                if result == 0 then
                    draws = draws + 1
                elseif result == 1 then
                    wins = wins + 1
                end
            end

            --[[ 
                Updates the fitness of the data row maximising victories ( 70% ) and
                minimizing draws ( 30% when 0 draws )
            ]]
            self.data[i][6] = (wins / #opp_moves)*0.7 + (1.0 - (draws / #opp_moves))*0.3
            dprint( wins, draws, self.data[i][6] )
        end

        -- Orders the rows so that the first row has the highest fitness
        table.sort( self.data, function( a,b ) return a[6] > b[6] end )

        --[[
            Now we mutate the solutions that have too low fitness value.
        ]]
        for i,row in pairs( self.data ) do
            if row[6] < 0.6 then
                self:mutate( i )
            end
        end
    end,

    --[[ Initially generates all the evolutionary data rows ]]
    generate = function( self, size )
        self.data = {}

        for i=1,size do
            self.data[i] = {}

            local total = 0
            for j=1,4 do
                total = math.random( total, 100 ) -- Lua rng works with integers
                self.data[i][j] = limit( 0,1, (total / 100.0) )
            end

            self.data[i][5] = 1 -- High limit for the random symbols intervals
            self.data[i][6] = 0 -- Starting fitness value
        end
    end,

    --[[
        Executes a mutation on the values of a specified data row,
        for now the mutation is just a modification of the values
        to 10% (increase or decrease) of their initial value.
    ]]
    mutate = function( self, index )
        for j=1,4 do
            local plus = 0

            -- randomly picks an increase or a decrease of 10%.
            if math.random() > 0.5 then
                plus = (self.data[index][j] * 0.1)
            else
                plus = -1 * (self.data[index][j] * 0.1)
            end

            self.data[index][j] = limit( 0,1, self.data[index][j] + plus )
        end
        self.data[index][5] = 1 -- the upper limit is always 1

        local tmp = self.data[index][6] -- The fitness value should not be ordered.
        self.data[index][6] = nil
        table.sort( self.data[index] ) -- The values can get mixed and so they're resorted.
        self.data[index][6] = tmp
    end,
}

main()