import random, sys

if __name__ == '__main__':

    # Graph in adjacency matrix here
    graph = {"S":"PL", "P":"VR", "R":"LS", "L":"VP", "V":"SR"}
    try:
        myHistory = sys.argv[1]
        opHistory = sys.argv[2]
        choices = ""

        # Insert some graph stuff here. Newer versions may include advanced Math.
        for v in graph:
            if opHistory[-1] == v:
                for u in graph:
                    if u in graph[v]:
                        choices += graph[u]

        print random.choice(choices + opHistory[-1])

    except:
        print random.choice("RPSLV")